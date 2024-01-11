using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class PromptParser
    {
        private static Regex re_attention = new Regex(@"\\\(|\\\)|\\\[|\\]|\\\\|\\|\(|\[|:\s*([+-]?[.\d]+)\s*\)|\)|]|[^\\()\[\]:]+|:", RegexOptions.Compiled | RegexOptions.Multiline);
        private static Regex re_break = new Regex(@"\s*\bBREAK\b\s*", RegexOptions.Compiled | RegexOptions.Multiline);

        public static float round_bracket_multiplier = 1.1f;
        public static float square_bracket_multiplier = 1f / 1.1f;

        public static List<PromptItem> ParsePrompt(string promptString, bool fixTagsForWeight, string splitSeparator = ",")
        {
            List<PromptItem> result = new List<PromptItem>();
            splitSeparator = splitSeparator.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
            if (fixTagsForWeight)
            {
                result = ParsePromptWeight(promptString, splitSeparator);
            }
            else
            {
                string[] tags = promptString.Split(new string[] { splitSeparator }, StringSplitOptions.RemoveEmptyEntries);
                if (tags.Length > 0)
                {
                    foreach (var tag in tags)
                    {
                        if (!string.IsNullOrWhiteSpace(tag))
                        {
                            string textTag = tag.ToLower().Trim();
                            int tagIndex = result.FindIndex(a => a.Text == textTag);
                            if (tagIndex == -1)
                            {
                                result.Add(new PromptItem(textTag, 1f));
                            }
                                
                        }
                    }
                }
            }
            return result;
        }

        public static List<PromptItem> ParsePromptWeight(string promptString, string splitSeparator = ",")
        {
            
            List<PromptItem> res = new List<PromptItem>();
            List<int> round_brackets = new List<int>();
            List<int> square_brackets = new List<int>();
            List<PromptItem> result = new List<PromptItem>();

            void multiply_range(int startPosition, float multiplier)
            {
                for (int i = startPosition; i < res.Count; i++)
                {
                    res[i].Weight *= multiplier;
                }
            }

            foreach (Match m in re_attention.Matches(promptString))
            {
                string text = m.Groups[0].Value;
                string weight = m.Groups[1].Value;

                if (text.StartsWith("\\"))
                    res.Add(new PromptItem(text.Substring(1), 1.0f));
                else if (text == "(")
                    round_brackets.Add(res.Count);
                else if (text == "[")
                    square_brackets.Add(res.Count);
                else if (!string.IsNullOrEmpty(weight) && round_brackets.Count > 0)
                    multiply_range(round_brackets.Pop(), (float)Convert.ToDouble(weight));
                else if (text == ")")
                    multiply_range(round_brackets.Pop(), round_bracket_multiplier);
                else if (text == "]")
                    multiply_range(square_brackets.Pop(), square_bracket_multiplier);
                else
                {
                    string[] parts = re_break.Split(text);
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if(i>0)
                            res.Add(new PromptItem("BREAK", -1f));
                        res.Add(new PromptItem(parts[i], 1.0f));
                    }
                }
            }
            foreach (var item in round_brackets)
            {
                multiply_range(item, round_bracket_multiplier);
            }
            foreach (var item in square_brackets)
            {
                multiply_range(item, square_bracket_multiplier);
            }

            if (res.Count == 0)
            {
                res.Add(new PromptItem("", 1f));
            }
            int p = 0;
            while (p + 1 < res.Count)
            {
                if (res[p].Weight == res[p + 1].Weight)
                {
                    res[p].Text += res[p + 1].Text;
                    res.RemoveAt(p + 1);
                }
                else
                    p += 1;
            }

            foreach (var item in res)
            {
                string[] clearedTags = item.Text.Split(new string[] {splitSeparator}, StringSplitOptions.RemoveEmptyEntries);
                if (clearedTags.Length > 0)
                {
                    foreach (var tag in clearedTags)
                    {
                        if (!string.IsNullOrWhiteSpace(tag))
                        {
                            string textTag = tag.Replace('_', ' ').ToLower().Trim();
                            int tagIndex = result.FindIndex(a => a.Text == textTag);
                            if (tagIndex != -1)
                            {
                                result[tagIndex].Weight *= round_bracket_multiplier * item.Weight;
                            }
                            else
                                result.Add(new PromptItem(textTag, item.Weight));
                        }
                    }
                }
            }

            return result;
        }



        public class PromptItem
        {
            public string Text { get; set; }
            public float Weight { get; set; }

            public PromptItem(string text, float weight)
            {
                Text = text;
                Weight = weight;
            }

            public override string ToString()
            {
                return $"{Text} : {Weight}";
            }
        }
    }
}
