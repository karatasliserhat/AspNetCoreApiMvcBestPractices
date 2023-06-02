using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlayerApi.Core.DTOs
{
    public class GetByCategoryWithProductDto:CategoryDto
    {
        public List<ProductDto> Products { get; set; }
    }
}
