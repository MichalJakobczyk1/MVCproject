using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVCproject.Models
{
    public class Employee : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Date of hire is required")]
        public DateTime HiredOn { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression("^[0-9]*$")]
        [Required(ErrorMessage = "Contact number is required")]
        public string ContactNumber { get; set; }

        [RegularExpression(".+\\@.+\\.[a-z]{2,3}")]
        public string Email { get; set; }
    }
}
