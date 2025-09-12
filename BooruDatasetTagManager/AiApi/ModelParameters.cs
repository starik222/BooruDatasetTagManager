using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    public class ModelParameters
    {
        public string ModelName { get; set; }
        public List<ModelAdditionalParameters> AdditionalParameters;

        public ModelParameters()
        {
            AdditionalParameters = new List<ModelAdditionalParameters>();
        }
    }
}
