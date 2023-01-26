using MVCproject.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateOfOrder { get; set; }
        public Suppliers Supplier { get; set; }
        public decimal Quantity { get; set; }
        public bool IsPaid { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public IEnumerable<Product> ProductsList { get; set; }
    }
}
