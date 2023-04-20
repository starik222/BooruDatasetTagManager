using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class ChineseTranslator : AbstractTranslator
    {

        private HttpClient client = new HttpClient();
        public ChineseTranslator() : base(TranslationService.ChineseTranslate)
        {
            client = new HttpClient();
        }

        public override async Task<string> TranslateAsync(string text, string fromLang, string toLang)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            toLang = toLang.Replace("-CN", "").Replace("-TW", "");
            FormUrlEncodedContent content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
                   {
                       new KeyValuePair<string, string>("appid","105"),
                       new KeyValuePair<string, string>("sgid",fromLang),
                       new KeyValuePair<string, string>("sbid",fromLang),
                       //new KeyValuePair<string, string>("sgid","auto"),
                       //new KeyValuePair<string, string>("sbid","auto"),
                       new KeyValuePair<string, string>("egid",toLang),
                       new KeyValuePair<string, string>("ebid",toLang),
                       new KeyValuePair<string, string>("content",text),
                       new KeyValuePair<string, string>("type","2"),
                   });
            var ret = await client.PostAsync($"https://translate-api-fykz.xiangtatech.com/translation/webs/index", content);
            if (ret.IsSuccessStatusCode)
            {
                string json = await ret.Content.ReadAsStringAsync();
                int begin = json.IndexOf("\"by\":\"");
                if (begin == -1)
                    return null;
                begin += 6;
                int end = json.IndexOf("\"", begin);
                if (end == -1)
                    return null;
                return json.Substring(begin, end - begin);
            }
            return null;
        }

        public override void Dispose()
        {
            client.Dispose();
        }
    }
}
