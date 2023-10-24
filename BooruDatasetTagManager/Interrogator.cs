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

        public async Task<InterrogateResult> InterrogateImage(string imagePath, List<NetworkInterrogationParameters> interrogationParameters)
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
                request.Params.AddRange(interrogationParameters);
                request.InterrogateImage = ByteString.CopyFrom(File.ReadAllBytes(imagePath));
                var response = await _client.InterrogateImageAsync(request);
                if (response.InterrogateOk)
                {
                    result.Success = true;
                    foreach (var item in response.Tags)
                    {
                        result.Items.Add(new AutoTagItem(item.Tag, item.Probability));
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
        public List<AutoTagItem> Items { get; set; }
        public string Message { get; set; }

        public InterrogateResult()
        {
            Items = new List<AutoTagItem>();
        }
    }

}
