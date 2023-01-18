using Microsoft.AspNetCore.Mvc;

namespace MVCproject.ViewModels
{
    public class EditReservationViewModel
    {
        public int Id { get; set; }
        public DateTime DateOfReservation { get; set; }
        public int HowManyPeople { get; set; }
    }
}
