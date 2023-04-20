using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class GoogleTranslator: AbstractTranslator
    {
        private string ch_zero;
        private HttpClient client;
        private const string googleTemplateUrl = "https://translate.google.com/m?hl=&sl={0}&tl={1}&ie=UTF-8&q={2}";
        public GoogleTranslator() : base(TranslationService.GoogleTranslate)
        {
            ch_zero = ((char)8203).ToString();
            client = new HttpClient();
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.3; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Linux; Android 10) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.5563.116 Mobile Safari/537.36");
            client.Timeout = new TimeSpan(0, 0, 10);
        }

        public override async Task<string> TranslateAsync(string text, string fromLang, string toLang)
        {
            string res =  await Translate(text, fromLang, toLang);
            await Task.Delay(500);//antispam:)
            return res;
        }



        private async Task<string> Translate(string str, string from, string to)
        {

            //string val = string.Format(googleTemplateUrl, from, to, ConvertStringToHex(str, Encoding.UTF8));
            string val = string.Format(googleTemplateUrl, from, to, str);
            string data = null;
            try
            {
                data = await client.GetStringAsync(val).ConfigureAwait(false);
                String extracted = data.GetBetween("class=\"result-container\">", "</div>");//<div class="result-container">тестовая строка</div>
                string text = WebUtility.HtmlDecode(extracted ?? string.Empty);
                if (string.IsNullOrEmpty(text))
                    return null;
                text = text.Replace(ch_zero, "");
                return ReplaceOtherChar(text);
            }
            catch (Exception)
            {
                return null;
            }

        }

        private string ReplaceOtherChar(string text)
        {
            int index = 0;
            string res = "";
            while (text.IndexOf("&#", index) != -1)
            {
                int old_ind = index;
                index = text.IndexOf("&#", index);
                res += text.Substring(old_ind, index);
                string chisl = text.Substring(index + 2, 2);
                byte[] ch = new byte[1];
                ch[0] = Convert.ToByte(chisl);
                string ret = Encoding.UTF8.GetString(ch);
                res += ret;
                index += 5;
            }
            if (index + 1 != text.Length)
            {
                res += text.Substring(index);
            }
            return res;
        }



        public override void Dispose()
        {
            client.Dispose();
        }
    }
}
