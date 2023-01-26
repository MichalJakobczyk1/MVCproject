using MVCproject.Data.Enum;
using MVCproject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.ViewModels
{
    public class PlaceOrderViewModel
    {
        public int Id { get; set; }
        public DateTime DateOfOrder { get; set; }
        public Suppliers Supplier { get; set; }
        public decimal Quantity { get; set; }
        public bool IsPaid { get; set; }
        public IEnumerable<Product> ProductsList { get; set; }
    }
}
