using System.ComponentModel.DataAnnotations;

namespace MVCproject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal? AlcVolume { get; set; }
    }
}
