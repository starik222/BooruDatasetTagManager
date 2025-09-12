using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    public class InterrogateImageRequest
    {
        public byte[] Image { get; set; }
        public bool SkipInternetRequests { get; set; }
        public bool SerializeVramUsage { get; set; }
        public string ImageName { get; set; }
        public List<ModelParameters> Models { get; set; }

    }
}
