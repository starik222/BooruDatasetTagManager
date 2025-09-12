using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    public class ModelParamResponse : BaseResponse
    {
        public string Type { get; set; }
        public List<ModelAdditionalParameters> Parameters { get; set; }
    }
}
