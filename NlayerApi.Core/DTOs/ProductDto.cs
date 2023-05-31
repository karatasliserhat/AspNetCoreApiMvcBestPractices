using NlayerApi.Core.Models;

namespace NlayerApi.Core.DTOs
{
    public class ProductDto:BaseDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Priace { get; set; }
        public int CategoryId { get; set; }
    }
}
