using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVCproject.Models
{
    public class Employee
    {
        [HiddenInput]
        public int id { get; set; }
        [Required(ErrorMessage = "Date of hire is required")]
        public DateTime hiredOn { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string surname { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime dateOfBirth { get; set; }
        [RegularExpression("^[0-9]*$")]
        [Required(ErrorMessage = "Contact number is required")]
        public string contactNumber { get; set; }
        [RegularExpression(".+\\@.+\\.[a-z]{2,3}")]
        public string email { get; set; }
    }
}
