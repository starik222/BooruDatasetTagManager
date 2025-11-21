using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager.AiApi
{
    public class AiOpenAiClient
    {
        public string ServerEndpoint { get; private set; }
        //public event Extensions.ErrorHandler ErrorMessage;

        private ChatClient chatClient = null;
        private ApiKeyCredential credential;
        private OpenAIClientOptions serverOptions;

        public bool IsConnected { get; private set; }


        public List<string> Models { get; private set; }

        public AiOpenAiClient(string srvEndpoint, string apiKey, int timeout)
        {
            ServerEndpoint = srvEndpoint;
            credential = new ApiKeyCredential(apiKey);
            serverOptions = new OpenAIClientOptions();
            serverOptions.Endpoint = new Uri(ServerEndpoint);
            serverOptions.NetworkTimeout = TimeSpan.FromSeconds(timeout);
            Models = new List<string>();

        }

        public async Task<(bool Result, string ErrMessage)> ConnectAsync()
        {
            try
            {
                OpenAIModelClient client = new OpenAIModelClient(credential, serverOptions);
                var models = await client.GetModelsAsync();
                Models.Clear();
                Models.AddRange(models.Value.Select(a => a.Id).Order());
                IsConnected = true;
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, "OpenAiClient connection error: " + ex.Message);
            }
        }

        public async Task<(string Result, string ErrMessage)> SendRequestAsync(OpenAiRequest request)
        {
            try
            {
                if (chatClient == null || chatClient.Model != request.Model)
                {
                    chatClient = new ChatClient(request.Model, credential, serverOptions);
                }
                List<ChatMessage> messages = new List<ChatMessage>();
                var chatOptions = new ChatCompletionOptions();
                if (!string.IsNullOrEmpty(request.SystemPrompt))
                    messages.Add(new SystemChatMessage(request.SystemPrompt));
                UserChatMessage userMessage = new UserChatMessage(request.UserPrompt);
                if (request.ImagePath != null)
                {
                    BinaryData bd = new BinaryData(await File.ReadAllBytesAsync(request.ImagePath));
                    string contentType = GetContentTypeFromExtention(Path.GetExtension(request.ImagePath));
                    ChatMessageContentPart partImage = ChatMessageContentPart.CreateImagePart(bd, contentType);
                    userMessage.Content.Add(partImage);
                }
                else if (request.ImageData != null)
                {
                    BinaryData bd = new BinaryData(request.ImageData);
                    ChatMessageContentPart partImage = ChatMessageContentPart.CreateImagePart(bd, request.ContentType);
                    userMessage.Content.Add(partImage);
                }
                messages.Add(userMessage);
                bool useChatOptions = false;
                if (request.RepeatPenalty != 0 || request.TopP != -1 || request.Temperature != -1)
                {
                    useChatOptions = true;
                    if (request.Temperature != -1)
                        chatOptions.Temperature = request.Temperature;
                    if (request.TopP != -1)
                        chatOptions.TopP = request.TopP;
                    if (request.RepeatPenalty != 0)
                        chatOptions.FrequencyPenalty = request.RepeatPenalty;
                }
                ChatCompletion result;
                if (useChatOptions)
                    result = await chatClient.CompleteChatAsync(messages, chatOptions);
                else
                    result = await chatClient.CompleteChatAsync(messages);
                if (result.FinishReason != ChatFinishReason.Stop)
                {
                    return (null, "OpenAiClient SendRequest return error: " + result.FinishReason.ToString());
                }
                return (result.Content[0].Text, "");
            }
            catch (Exception ex)
            {
                return (null, "OpenAiClient SendRequest error: " + ex.Message);
            }
        }

        private string GetContentTypeFromExtention(string ext)
        {
            switch (ext.ToLower())
            {
                case ".jpg":
                    {
                        return "image/jpeg";
                    }
                case ".bmp":
                    {
                        return "image/bmp";
                    }
                case ".png":
                    {
                        return "image/png";
                    }
                case ".gif":
                    {
                        return "image/gif";
                    }
                case ".webp":
                    {
                        return "image/webp";
                    }
                default:
                    {
                        return "application/octet-stream";
                    }
            }
        }

        public async Task<(List<AiApiClient.AutoTagItem> data, string errorMessage)> GetTagsWithAutoTagger(string imagePath, bool defSettings)
        {
            if (!defSettings || Program.OpenAiAutoTagger == null || string.IsNullOrEmpty(Program.Settings.OpenAiAutoTagger.Model))
            {
                Form_AutoTaggerOpenAiSettings autoTaggerSettings = new Form_AutoTaggerOpenAiSettings();
                if (autoTaggerSettings.ShowDialog() != DialogResult.OK || Program.OpenAiAutoTagger == null || string.IsNullOrEmpty(Program.Settings.OpenAiAutoTagger.Model))
                {
                    autoTaggerSettings.Close();
                    return (new List<AiApiClient.AutoTagItem>(), I18n.GetText("TipGenCancel"));
                }
            }
            if (!Program.OpenAiAutoTagger.IsConnected)
            {
                var connectionResult = await Program.OpenAiAutoTagger.ConnectAsync();
                if (!connectionResult.Result)
                {
                    return (null, connectionResult.ErrMessage);
                }
            }

            OpenAiRequest request = new OpenAiRequest();
            request.Temperature = Program.Settings.OpenAiAutoTagger.Temperature;
            request.UserPrompt = Program.Settings.OpenAiAutoTagger.UserPrompt;
            request.TopP = Program.Settings.OpenAiAutoTagger.TopP;
            request.SystemPrompt = Program.Settings.OpenAiAutoTagger.SystemPrompt;
            request.Model = Program.Settings.OpenAiAutoTagger.Model;
            request.RepeatPenalty = Program.Settings.OpenAiAutoTagger.RepeatPenalty;
            string imgExt = Path.GetExtension(imagePath).ToLower();
            if (Extensions.VideoExtensions.Contains(imgExt) || imgExt == ".webp")
            {
                request.ImageData = Extensions.ImageToByteArray(Extensions.GetImageFromFile(imagePath));
                request.ContentType = "image/png";
            }
            else
                request.ImagePath = imagePath;


            var response = await Program.OpenAiAutoTagger.SendRequestAsync(request);
            string errMess = response.ErrMessage;
            if (response.Result == null)
            {
                return (null, errMess);
            }
            List<AiApiClient.AutoTagItem> result = new List<AiApiClient.AutoTagItem>();
            if (Program.Settings.OpenAiAutoTagger.SplitString)
            {
                result = response.Result.Split(Program.Settings.OpenAiAutoTagger.Splitter, StringSplitOptions.RemoveEmptyEntries).Select(a=>new AiApiClient.AutoTagItem(a.Trim(), 1f)).ToList();
            }
            else
            {
                result.Add(new AiApiClient.AutoTagItem(response.Result, 1f));
            }

            if (Program.Settings.OpenAiAutoTagger.TagFilteringMode != TagFilteringMode.None && !string.IsNullOrEmpty(Program.Settings.OpenAiAutoTagger.TagFilter))
            {
                if (Program.Settings.OpenAiAutoTagger.TagFilteringMode == TagFilteringMode.Regex)
                    try
                    {
                        result = result.Where(t => Regex.IsMatch(t.Tag, Program.Settings.OpenAiAutoTagger.TagFilter, RegexOptions.IgnoreCase)).ToList();
                    }
                    catch
                    {
                        errMess = I18n.GetText("TipInvalidRegex");
                    }
                else
                {
                    string[] tagFilter = Program.Settings.OpenAiAutoTagger.TagFilter.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    if (Program.Settings.OpenAiAutoTagger.TagFilteringMode == TagFilteringMode.Equal)
                        result = result.Where(t => tagFilter.Any(f => string.Equals(t.Tag, f, StringComparison.OrdinalIgnoreCase))).ToList();
                    else if (Program.Settings.OpenAiAutoTagger.TagFilteringMode == TagFilteringMode.NotEqual)
                        result = result.Where(t => !tagFilter.Any(f => string.Equals(t.Tag, f, StringComparison.OrdinalIgnoreCase))).ToList();
                    else if (Program.Settings.OpenAiAutoTagger.TagFilteringMode == TagFilteringMode.Containing)
                        result = result.Where(t => tagFilter.Any(f => t.Tag.Contains(f, StringComparison.OrdinalIgnoreCase))).ToList();
                    else if (Program.Settings.OpenAiAutoTagger.TagFilteringMode == TagFilteringMode.NotContaining)
                        result = result.Where(t => !tagFilter.Any(f => t.Tag.Contains(f, StringComparison.OrdinalIgnoreCase))).ToList();
                }
            }

            if (Program.Settings.OpenAiAutoTagger.SortMode == AutoTaggerSort.Confidence)
            {
                result.Sort((a, b) => b.Confidence.CompareTo(a.Confidence));
            }
            else if (Program.Settings.OpenAiAutoTagger.SortMode == AutoTaggerSort.Alphabetical)
            {
                result.Sort((a, b) => a.Tag.CompareTo(b.Tag));
            }
            return (result, errMess);
        }
    }



    public class OpenAiRequest
    {
        public string Model { get; set; } = null;
        public string SystemPrompt { get; set; } = null;
        public string UserPrompt { get; set; } = string.Empty;
        public string ImagePath { get; set; } = null;
        public byte[] ImageData { get; set; } = null;
        public string ContentType { get; set; } = null;
        public float Temperature { get; set; } = -1;
        public float TopP { get; set; } = -1;
        public float RepeatPenalty { get; set; } = 0;

    }
}
