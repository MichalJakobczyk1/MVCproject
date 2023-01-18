using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.Models
{
    public class Order
    {
        [Key]
        public string Id { get; set; }
        public string Supplier { get; set; }
        public decimal PriceTotal { get; set; }
        public bool IsPaid { get; set; }
        [ForeignKey("Product")]
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
