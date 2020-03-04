using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTemplate2
{
    class AdditionalFeatures
    {
        //Define Properties
        public string FeatureType { get; set; }
        

        //Default Constructor
        public AdditionalFeatures() { }

        //Another Constructor
        public AdditionalFeatures(string featuretype)
        {
            FeatureType = featuretype;
            
        }
    }
}
