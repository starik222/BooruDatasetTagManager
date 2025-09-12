using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    class InterrogateImageResponse : BaseResponse
    {
        public List<InterrogateImageResult> Result { get; set; }
    }
}
