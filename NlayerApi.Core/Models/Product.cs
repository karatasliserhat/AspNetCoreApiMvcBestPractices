using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlayerApi.Core.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Priace { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ProductFeatureId { get; set; }
        public ProductFeature ProductFeature { get; set; }
    }
}
