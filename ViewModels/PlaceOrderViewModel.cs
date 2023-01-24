using MVCproject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.ViewModels
{
    public class PlaceOrderViewModel
    {
        public int Id { get; set; }
        public string Supplier { get; set; }
        public decimal Quantity { get; set; }
        public bool IsPaid { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
