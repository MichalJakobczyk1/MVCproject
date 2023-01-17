using System.ComponentModel.DataAnnotations;

namespace MVCproject.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateOfReservation { get; set; }
        public int HowManyPeople { get; set; }
    }
}
