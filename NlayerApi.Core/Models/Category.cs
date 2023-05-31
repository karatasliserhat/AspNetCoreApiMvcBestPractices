namespace NlayerApi.Core.Models
{
    public class Category:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
