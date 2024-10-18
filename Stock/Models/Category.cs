using System.Text.Json.Serialization;

namespace Stock.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual List<Product> products { get; set; } = new List<Product>();
    }
}
