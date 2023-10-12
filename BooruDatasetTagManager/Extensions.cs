using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Translator.Crypto;
using System.Drawing;

namespace BooruDatasetTagManager
{
    public static class Extensions
    {

        public static void AddRange(this List<TagValue> list, IEnumerable<string> range)
        {
            foreach (var item in range)
                list.Add(new TagValue(item));
        }

        public static long GetHash(this string text)
        {
            return Adler32.GenerateHash(text);
        }

        public static object LoadDataSet(string path)
        {
            MemoryStream ms = new MemoryStream(File.ReadAllBytes(path));
            var obj = new BinaryFormatter().Deserialize((Stream)ms);
            ms.Close();
            return obj;
        }

        public static object LoadDataSet(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            var obj = new BinaryFormatter().Deserialize((Stream)ms);
            ms.Close();
            return obj;
        }


        public static void SaveDataSet(object lst, string path)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, lst);
            File.WriteAllBytes(path, ms.ToArray());
            ms.Close();
        }

        public static byte[] SaveDataSet(object objItem)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, objItem);
                return ms.ToArray();
            }
        }

        public static string GetBetween(this string strSource, string strStart, string strEnd)
        {
            const int kNotFound = -1;

            var startIdx = strSource.IndexOf(strStart);
            if (startIdx != kNotFound)
            {
                startIdx += strStart.Length;
                var endIdx = strSource.IndexOf(strEnd, startIdx);
                if (endIdx > startIdx)
                {
                    return strSource.Substring(startIdx, endIdx - startIdx);
                }
            }
            return String.Empty;
        }

        public static string GetBetween(this string strSource, string strStart, string strEnd, int startIndex)
        {
            const int kNotFound = -1;

            var startIdx = strSource.IndexOf(strStart, startIndex);
            if (startIdx != kNotFound)
            {
                startIdx += strStart.Length;
                var endIdx = strSource.IndexOf(strEnd, startIdx);
                if (endIdx > startIdx)
                {
                    return strSource.Substring(startIdx, endIdx - startIdx);
                }
            }
            return String.Empty;
        }

        public static Image GetImageFromFile(string imagePath)
        {
            bool isWebP = false;
            using (FileStream fs = new FileStream(imagePath, FileMode.Open))
            {
                if (fs.Length < 4)
                    return null;
                byte[] signature = new byte[4];
                fs.Read(signature, 0, 4);
                if (BitConverter.ToInt32(signature, 0) == 1179011410 || BitConverter.ToInt32(signature, 0) == 1346520407)
                    isWebP = true;
            }

            if (!isWebP)
            {
                using (var img = Image.FromFile(imagePath))
                {
                    return new Bitmap(img);
                }
            }
            else
            {
                using (WebPWrapper.WebP wp = new WebPWrapper.WebP())
                {
                    return wp.Load(imagePath);
                }
            }
        }

        public static Bitmap Transparent2Color(Bitmap bmp1, Color target)
        {
            Bitmap bmp2 = new Bitmap(bmp1.Width, bmp1.Height);
            Rectangle rect = new Rectangle(Point.Empty, bmp1.Size);
            using (Graphics G = Graphics.FromImage(bmp2))
            {
                G.Clear(target);
                G.DrawImageUnscaledAndClipped(bmp1, rect);
            }
            return bmp2;
        }

        public static Image MakeThumb(string imagePath, int imgSize)
        {

            using (var img = Extensions.GetImageFromFile(imagePath))
            {
                var aspect = img.Width / (float)img.Height;

                int newHeight = img.Height * imgSize / img.Width;
                int newWidth = imgSize;

                if (newHeight > imgSize)
                {
                    newWidth = img.Width * imgSize / img.Height;
                    newHeight = imgSize;
                }

                return img.GetThumbnailImage(newWidth, newHeight, () => false, IntPtr.Zero);
            }
        }


        public static T Pop<T>(this List<T> list)
        {
            if(list == null || list.Count == 0)
                return default;
            T res = list[list.Count - 1];
            //T res = list[0];
            list.RemoveAt(list.Count - 1);
            //list.RemoveAt(0);
            return res;
        }

    }
}
