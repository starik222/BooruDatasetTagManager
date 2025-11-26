using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diffusion.IO
{
    public class StealthPng
    {
        public static string Read(Stream stream)
        {
            bool confirmingSignature = true;
            bool sigConfirmed = false;
            bool readingParamLen = false;
            bool readingParam = false;
            bool readEnd = false;
            bool compressed = false;
            string mode = "";
            int indexA = 0, indexRgb = 0, paramLen = 0;
            StringBuilder bufferA = new StringBuilder();
            StringBuilder bufferRgb = new StringBuilder();
            string binaryData = "";
            string genInfo = "";


            using (Bitmap bitmap = new Bitmap(stream))
            {
                bool hasAlpha = bitmap.PixelFormat is PixelFormat.Format24bppRgb or PixelFormat.Format32bppArgb;

                for (int x = 0; x < bitmap.Width; x++)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {

                        Color pixel = bitmap.GetPixel(x, y);

                        int r = pixel.R;
                        int g = pixel.G;
                        int b = pixel.B;

                        if (hasAlpha)
                        {
                            int a = pixel.A;
                            bufferA.Append(a & 1);
                            indexA++;
                        }

                        bufferRgb.Append(r & 1).Append(g & 1).Append(b & 1);
                        indexRgb += 3;

                        if (confirmingSignature)
                        {
                            if (indexA == "stealth_pnginfo".Length * 8)
                            {
                                string decodedSig = DecodeBinaryString(bufferA.ToString());
                                if (decodedSig == "stealth_pnginfo" || decodedSig == "stealth_pngcomp")
                                {
                                    confirmingSignature = false;
                                    sigConfirmed = true;
                                    readingParamLen = true;
                                    mode = "alpha";
                                    if (decodedSig == "stealth_pngcomp") compressed = true;
                                    bufferA.Clear();
                                    indexA = 0;
                                }
                                else
                                {
                                    readEnd = true;
                                    break;
                                }
                            }
                            else if (indexRgb == "stealth_pnginfo".Length * 8)
                            {
                                string decodedSig = DecodeBinaryString(bufferRgb.ToString());
                                if (decodedSig == "stealth_rgbinfo" || decodedSig == "stealth_rgbcomp")
                                {
                                    confirmingSignature = false;
                                    sigConfirmed = true;
                                    readingParamLen = true;
                                    mode = "rgb";
                                    if (decodedSig == "stealth_rgbcomp") compressed = true;
                                    bufferRgb.Clear();
                                    indexRgb = 0;
                                }
                            }
                        }
                        else if (readingParamLen)
                        {
                            if (mode == "alpha" && indexA == 32)
                            {
                                paramLen = Convert.ToInt32(bufferA.ToString(), 2);
                                readingParamLen = false;
                                readingParam = true;
                                bufferA.Clear();
                                indexA = 0;
                            }
                            else if (mode == "rgb" && indexRgb == 33)
                            {
                                char pop = bufferRgb[^1];
                                bufferRgb.Remove(bufferRgb.Length - 1, 1);
                                paramLen = Convert.ToInt32(bufferRgb.ToString(), 2);
                                readingParamLen = false;
                                readingParam = true;
                                bufferRgb.Clear().Append(pop);
                                indexRgb = 1;
                            }
                        }
                        else if (readingParam)
                        {
                            if (mode == "alpha" && indexA == paramLen)
                            {
                                binaryData = bufferA.ToString();
                                readEnd = true;
                                break;
                            }
                            else if (mode == "rgb" && indexRgb >= paramLen)
                            {
                                int diff = paramLen - indexRgb;
                                if (diff < 0)
                                {
                                    bufferRgb.Remove(bufferRgb.Length + diff, -diff);
                                }

                                binaryData = bufferRgb.ToString();
                                readEnd = true;
                                break;
                            }
                        }
                        else
                        {
                            readEnd = true;
                            break;
                        }
                    }

                    if (readEnd) break;
                }

                if (sigConfirmed && !string.IsNullOrEmpty(binaryData))
                {
                    try
                    {
                        byte[] byteData = ConvertBinaryToByteArray(binaryData);
                        if (compressed)
                        {
                            genInfo = DecompressGzip(byteData);
                        }
                        else
                        {
                            genInfo = Encoding.UTF8.GetString(byteData);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                return genInfo;
            }

            static string DecodeBinaryString(string binary)
            {
                byte[] bytes = ConvertBinaryToByteArray(binary);
                return Encoding.UTF8.GetString(bytes);
            }

            static byte[] ConvertBinaryToByteArray(string binary)
            {
                return Enumerable.Range(0, binary.Length / 8)
                    .Select(i => Convert.ToByte(binary.Substring(i * 8, 8), 2))
                    .ToArray();
            }

            static string DecompressGzip(byte[] compressedData)
            {
                using var inputStream = new MemoryStream(compressedData);
                using var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress);
                using var reader = new StreamReader(gZipStream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

    }
}
