using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    public class AiOpenAiClient
    {
        public string ServerEndpoint { get; private set; }
        public event Extensions.ErrorHandler ErrorMessage;

        private ChatClient chatClient = null;
        private ApiKeyCredential credential;
        private OpenAIClientOptions serverOptions;


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

        public async Task<bool> ConnectAsync()
        {
            try
            {
                OpenAIModelClient client = new OpenAIModelClient(credential, serverOptions);
                var models = await client.GetModelsAsync();
                Models.Clear();
                Models.AddRange(models.Value.Select(a => a.Id).Order());
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage?.Invoke("OpenAiClient connection error: " + ex.Message);
                return false;
            }
        }

        public async Task<string> SendRequestAsync(OpenAiRequest request)
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
                messages.Add(userMessage);
                bool useChatOptions = false;
                if (request.RepeatPenalty != 0 || request.TopP != -1 || request.Temperature != -1)
                {
                    useChatOptions = true;
                    chatOptions.Temperature = request.Temperature;
                    chatOptions.TopP = request.TopP;
                    chatOptions.FrequencyPenalty = request.RepeatPenalty;
                }
                ChatCompletion result;
                if (useChatOptions)
                    result = await chatClient.CompleteChatAsync(messages, chatOptions);
                else
                    result = await chatClient.CompleteChatAsync(messages);
                if (result.FinishReason != ChatFinishReason.Stop)
                {
                    ErrorMessage?.Invoke("OpenAiClient SendRequest return error: " + result.FinishReason.ToString());
                    return null;
                }
                return result.Content[0].Text;
            }
            catch (Exception ex)
            {
                ErrorMessage?.Invoke("OpenAiClient SendRequest error: " + ex.Message);
                return null;
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
    }



    public class OpenAiRequest
    {
        public string Model { get; set; } = null;
        public string SystemPrompt { get; set; } = null;
        public string UserPrompt { get; set; } = string.Empty;
        public string ImagePath { get; set; } = null;
        public float Temperature { get; set; } = -1;
        public float TopP { get; set; } = -1;
        public float RepeatPenalty { get; set; } = 0;

    }
}
