using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using System.IO;
using Image_Interrogator_Ns;
using Google.Protobuf.Collections;

namespace BooruDatasetTagManager
{
    public class Interrogator : IDisposable
    {
        private ImageInterrogator.ImageInterrogatorClient _client;
        private GrpcChannel _channel;
        public List<string> InterrogatorList;

        public bool IsConnected { get; private set; }
        public Interrogator()
        {
            _channel = GrpcChannel.ForAddress("http://127.0.0.1:50051");
            _client = new ImageInterrogator.ImageInterrogatorClient(_channel);
            InterrogatorList = new List<string>();
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                var request = new InterrogatorListingRequest();
                var response = await _client.ListInterrogatorsAsync(request);
                InterrogatorList = response.InterrogatorNames.Cast<string>().ToList();
                if (InterrogatorList.Count > 0)
                {
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

        public async Task<InterrogateResult> InterrogateImage(string imagePath, List<NetworkInterrogationParameters> interrogationParameters, bool serializeVramUsage, bool SkipInternetRequests)
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
                InterrogationRequest request = new InterrogationRequest();
                request.SerializeVramUsage = serializeVramUsage;
                request.SkipInternetRequests = SkipInternetRequests;
                request.Params.AddRange(interrogationParameters);
                request.InterrogateImage = ByteString.CopyFrom(File.ReadAllBytes(imagePath));
                request.ImageName = Path.GetFileName(imagePath);
                var response = await _client.InterrogateImageAsync(request);
                if (response.InterrogateOk)
                {
                    result.Success = true;
                    foreach (var net in response.Responses)
                    {
                        List<AutoTagItem> items = new List<AutoTagItem>();
                        foreach (var item in net.Tags)
                        {
                            items.Add(new AutoTagItem(Program.Settings.FixTagsOnSaveLoad ? item.Tag.Replace('_', ' ') : item.Tag, item.Probability));
                        }
                        result.Items[net.NetworkName] = items;
                    }
                    result.Message = response.ErrorMsg;
                    return result;
                }
                else
                {
                    result.Success = false;
                    result.Message = response.ErrorMsg;
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

        public async Task<InterrogatorParamResponse> GetInterrogatorParams(string name)
        {
            InterrogatorParamRequest request = new InterrogatorParamRequest()
            {
                InterrogatorNetwork = name
            };
            InterrogatorParamResponse resp;
            return await _client.InterrogatorParametersAsync(request);
        }

        public void Dispose()
        {
            _channel.Dispose();
        }
    }

    //public class NetworkInterrogationParameters
    //{
    //    public string InterrogatorName { get; set; }
    //    public float Threshold { get; set; }

    //    public NetworkInterrogationParameters(string interrogatorName, float threshold)
    //    {
    //        InterrogatorName = interrogatorName;
    //        Threshold = threshold;
    //    }
    //}


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
                //List<AutoTagItem> result = new List<AutoTagItem>();
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
                            preResult[item.Tag] = new MultitagUnionData(){ Count = 1, Confidence = item.Confidence };
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

}
