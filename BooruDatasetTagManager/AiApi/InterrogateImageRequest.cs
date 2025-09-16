using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    public class InterrogateImageRequest
    {
        public byte[] DataObject { get; set; }
        public ObjectDataType DataType { get; set; }
        public bool SkipInternetRequests { get; set; }
        public bool SerializeVramUsage { get; set; }
        public string FileName { get; set; }
        public List<ModelParameters> Models { get; set; }

    }
}
