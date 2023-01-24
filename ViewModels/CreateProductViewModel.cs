using MVCproject.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace MVCproject.ViewModels
{
    public class CreateProductViewModel
    {
        [Key]
        public int Id { get; set; }
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Products Category { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Volume is required")]
        public decimal Volume { get; set; }
        public decimal? AlcVolume { get; set; }
    }
}
