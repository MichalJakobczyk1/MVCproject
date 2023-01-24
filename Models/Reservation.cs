using System.ComponentModel.DataAnnotations;

namespace MVCproject.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime DateOfReservation { get; set; }

        [Required(ErrorMessage = "Quantity of people is required")]
        public int HowManyPeople { get; set; }
    }
}
