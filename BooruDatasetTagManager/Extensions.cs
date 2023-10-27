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
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Data;

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

        public static int CalcBracketsCount(float weight, bool positive)
        {
            if (weight == 1 || weight == 0)
                return 0;
            int count = 0;
            float mult = positive ? PromptParser.round_bracket_multiplier : PromptParser.square_bracket_multiplier;

            if (positive)
            {
                while (weight > 1)
                {
                    weight /= mult;
                    count++;
                }
            }
            else
            {
                while (weight < 1)
                {
                    weight /= mult;
                    count++;
                }
            }
            if (weight == 1)
                return count;
            else
                return 0;
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
            byte[] imageData = File.ReadAllBytes(imagePath);
            if (imageData.Length < 4)
                return null;
            if (BitConverter.ToInt32(imageData, 0) == 1179011410 || BitConverter.ToInt32(imageData, 0) == 1346520407)
                isWebP = true;
            if (!isWebP)
            {
                return Image.FromStream(new MemoryStream(imageData));
            }
            else
            {
                using (WebPWrapper.WebP wp = new WebPWrapper.WebP())
                {
                    return wp.Load(imageData);
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


        public static async void CheckForUpdateAsync(string currentVersion)
        {
            string data = null;
            await Task.Run(async () =>
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Accept", "application/vnd.github+jso");
                        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
                        client.DefaultRequestHeaders.Add("User-Agent", "BooruDatasetTagManager");
                        data = await client.GetStringAsync("https://api.github.com/repos/starik222/BooruDatasetTagManager/releases/latest");
                    }
                }
                catch (Exception)
                {
                    return;
                }
            });
            if (!string.IsNullOrWhiteSpace(data))
            {
                try
                {
                    JObject jsonData = JObject.Parse(data);
                    string version = jsonData["tag_name"].ToString();
                    if (version.StartsWith("v"))
                        version = version.Substring(1);
                    if (currentVersion.StartsWith(version))
                    {
                        return;
                    }
                    string text = jsonData["body"].ToString();
                    string url = jsonData["html_url"].ToString();
                    string[] listItems = text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 1; i < listItems.Length; i++)
                    {
                        sb.AppendLine(listItems[i]);
                    }
                    if (MessageBox.Show($"A new version of the program has been detected ({version}).\nNew in version:\n{sb}\nDo you want to go to the program download page?",
                        "Software update found", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = url,
                            UseShellExecute = true
                        });
                    }
                }
                catch (Exception)
                {
                    return;
                }
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
