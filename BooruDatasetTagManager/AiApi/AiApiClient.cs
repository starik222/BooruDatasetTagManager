using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    public class AiApiClient : IDisposable
    {
        private readonly HttpClient client = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        });

        private string connetionAddress = string.Empty;
        public bool IsConnected { get; private set; }

        public ConfigResponse Config {get; private set; } = new ConfigResponse();

        public AiApiClient()
        {
            client.Timeout = TimeSpan.FromSeconds(500);
            connetionAddress = Program.Settings.AutoTagger.ConnectionAddress;
            if(!connetionAddress.EndsWith('/'))
                connetionAddress += "/";
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                var response = await GetJsonAsync("getconfig");
                if (response.Success)
                {
                    Config = GetObjectFromResponse<ConfigResponse>(response.JsonText);
                    IsConnected = true;
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<InterrogateResult> InterrogateImage(string imagePath, List<ModelParameters> models, bool serializeVramUsage, bool SkipInternetRequests)
        {
            InterrogateResult result = new InterrogateResult();
            if (!File.Exists(imagePath) || !IsConnected)
            {
                result.Success = false;
                result.Message = IsConnected ? "Image not found" : "The connection to the service has not been established!";
                return result;
            }
            try
            {
                InterrogateImageRequest request = new InterrogateImageRequest();
                request.SerializeVramUsage = serializeVramUsage;
                request.SkipInternetRequests = SkipInternetRequests;
                request.Models = models;
                request.Image = File.ReadAllBytes(imagePath);
                request.ImageName = Path.GetFileName(imagePath);
                var response = await PostJsonAsync("interrogateimage", request);
                if (response.Success)
                {
                    result.Success = true;
                    var jsonResp = GetObjectFromResponse<InterrogateImageResponse>(response.JsonText);
                    if (jsonResp.Success)
                    {
                        foreach (var net in jsonResp.Result)
                        {
                            List<AutoTagItem> items = new List<AutoTagItem>();
                            foreach (var item in net.Tags)
                            {
                                items.Add(new AutoTagItem(Program.Settings.FixTagsOnSaveLoad ? item.Tag.Replace('_', ' ') : item.Tag, item.Probability));
                            }
                            result.Items[net.ModelName] = items;
                        }
                        result.Message = response.ErrorMessage;
                        return result;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = jsonResp.ErrorMessage;
                        return result;
                    }

                }
                else
                {
                    result.Success = false;
                    result.Message = response.ErrorMessage;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<EditResult> EditImage(string imagePath, ModelParameters model, bool serializeVramUsage, bool SkipInternetRequests)
        {
            EditResult result = new EditResult();
            if (!File.Exists(imagePath) || !IsConnected)
            {
                result.Success = false;
                result.Message = IsConnected ? "Image not found" : "The connection to the service has not been established!";
                return result;
            }
            try
            {
                EditImageRequest request = new EditImageRequest();
                request.SerializeVramUsage = serializeVramUsage;
                request.SkipInternetRequests = SkipInternetRequests;
                request.Model = model;
                request.Image = File.ReadAllBytes(imagePath);
                request.ImageName = Path.GetFileName(imagePath);

                var response = await PostJsonAsync("editimage", request);
                if (response.Success)
                {
                    result.Success = true;
                    var jsonResp = GetObjectFromResponse<EditImageResponse>(response.JsonText);
                    if (jsonResp.Success)
                    {
                        result.Success = true;
                        result.ImageData = jsonResp.Image;
                        result.Message = jsonResp.ErrorMessage;
                        return result;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = jsonResp.ErrorMessage;
                        return result;
                    }

                }
                else
                {
                    result.Success = false;
                    result.Message = response.ErrorMessage;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ModelParamResponse> GetModelParams(string name)
        {
            ModelParamResponse result = new ModelParamResponse();
            JObject root = new JObject();
            root["Name"] = name;
            var response = await PostJsonAsync("getmodelparams", root.ToString());
            if (response.Success)
            {
                return GetObjectFromResponse<ModelParamResponse>(response.JsonText);

            }
            else
            {
                result.Success = false;
                result.ErrorMessage = response.ErrorMessage;
                return result;
            }
        }

        public async Task<ConfigResponse> GetListModelsByType(string modelType)
        {
            ConfigResponse result = new ConfigResponse();
            var response = await GetJsonAsync("listmodelsbytype", $"name={modelType}");
            if (response.Success)
            {
                result = GetObjectFromResponse<ConfigResponse>(response.JsonText);
            }
            return result;
        }

        public class AutoTagItem
        {
            public string Tag { get; set; }
            public float Confidence { get; set; }

            public AutoTagItem() { }

            public AutoTagItem(string tag, float confidence)
            {
                Tag = tag;
                Confidence = confidence;
            }
        }

        private class MultitagUnionData
        {
            public int Count { get; set; }
            public float Confidence { get; set; }
        }

        public class InterrogateResult
        {
            public bool Success { get; set; }
            public Dictionary<string, List<AutoTagItem>> Items { get; set; }
            public string Message { get; set; }

            public InterrogateResult()
            {
                Items = new Dictionary<string, List<AutoTagItem>>();
            }

            public List<AutoTagItem> GetTagList(NetworkUnionMode unionMode)
            {
                if (Items.Count == 0)
                    return new List<AutoTagItem>();
                else if (Items.Count == 1)
                    return Items.ElementAt(0).Value;
                else
                {
                    Dictionary<string, MultitagUnionData> preResult = new Dictionary<string, MultitagUnionData>();
                    List<AutoTagItem> result = new List<AutoTagItem>();
                    foreach (var net in Items)
                    {
                        foreach (var item in net.Value)
                        {
                            if (preResult.ContainsKey(item.Tag))
                            {
                                preResult[item.Tag].Confidence = (preResult[item.Tag].Confidence + item.Confidence) / 2;
                                preResult[item.Tag].Count++;
                            }
                            else
                                preResult[item.Tag] = new MultitagUnionData() { Count = 1, Confidence = item.Confidence };
                        }
                    }


                    if (unionMode == NetworkUnionMode.Addition)
                    {
                        foreach (var item in preResult)
                        {
                            result.Add(new AutoTagItem(item.Key, item.Value.Confidence));
                        }
                    }
                    else if (unionMode == NetworkUnionMode.Subtraction)
                    {
                        foreach (var item in preResult)
                        {
                            if (item.Value.Count == 1)
                                result.Add(new AutoTagItem(item.Key, item.Value.Confidence));
                        }
                    }
                    else if (unionMode == NetworkUnionMode.Intersection)
                    {
                        foreach (var item in preResult)
                        {
                            if (item.Value.Count == Items.Count)
                                result.Add(new AutoTagItem(item.Key, item.Value.Confidence));
                        }
                    }
                    return result;
                }
            }

            private class MultitagUnionData
            {
                public int Count { get; set; }
                public float Confidence { get; set; }
            }
        }

        public class EditResult
        {
            public bool Success { get; set; }
            public byte[] ImageData { get; set; }
            public string Message { get; set; }
        }

        #region privateMethods
        private async Task<BaseResponse> PostJsonAsync(string path, string jsonText)
        {
            BaseResponse result = new BaseResponse();
            try
            {
                string url = connetionAddress + path;
                using StringContent jsonContent = new(
                    jsonText,
                    Encoding.UTF8,
                    "application/json");

                using HttpResponseMessage response = await client.PostAsync(
                    url,
                    jsonContent);

                response.EnsureSuccessStatusCode();
                result.JsonText = await response.Content.ReadAsStringAsync();
                result.ErrorMessage = "Success!";
                result.Success = true;
            }
            catch (HttpRequestException ex)
            {
                result.ErrorMessage = ex.Message;
                result.Success = false;
            }
            return result;
        }

        private async Task<BaseResponse> PostJsonAsync(string path, object jsonObject)
        {
            return await PostJsonAsync(path, JsonConvert.SerializeObject(jsonObject));
        }

        private async Task<BaseResponse> GetJsonAsync(string path, string query = "")
        {
            BaseResponse result = new BaseResponse();
            try
            {
                string url = connetionAddress + path;
                if (!string.IsNullOrEmpty(query))
                {
                    url += "?" + query;
                }
                using HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();
                result.JsonText = await response.Content.ReadAsStringAsync();
                result.ErrorMessage = "Success!";
                result.Success = true;
            }
            catch (HttpRequestException ex)
            {
                result.ErrorMessage = ex.Message;
                result.Success = false;
            }
            return result;
        }

        private T GetObjectFromResponse<T>(string response)
        {
            return JsonConvert.DeserializeObject<T>(response);
        }

        #endregion
    }
}
