using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using System.IO;

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

        public async Task<List<AutoTagItem>> InterrogateImage(string imagePath, string InterrogatorName, float? threshold)
        {
            if (!File.Exists(imagePath) || !IsConnected)
                return null;
            try
            {
                InterrogationRequest request = new InterrogationRequest();
                request.InterrogatorNetwork = InterrogatorName;
                if (threshold != null)
                    request.InterrogatorThreshold = threshold.Value;
                request.InterrogateImage = ByteString.CopyFrom(File.ReadAllBytes(imagePath));
                var response = await _client.InterrogateImageAsync(request);
                if (response.InterrogateOk)
                {
                    List<AutoTagItem> result = new List<AutoTagItem>();
                    foreach (var item in response.Tags)
                    {
                        result.Add(new AutoTagItem(item.Tag, item.Probability));
                    }
                    return result;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Dispose()
        {
            _channel.Dispose();
        }
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
}
