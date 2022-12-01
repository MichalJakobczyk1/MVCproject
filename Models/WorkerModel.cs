using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVCproject.Models
{
    public class Worker
    {
        [HiddenInput]
        public int id { get; set; }
        [Required(ErrorMessage = "Date of hire is required")]
        public DateTime hiredOn { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string surname { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int age { get; set; }
        [RegularExpression("^[0-9]*$")]
        [Required(ErrorMessage = "Contact number is required")]
        public string contactNumber { get; set; }
        [RegularExpression(".+\\@.+\\.[a-z]{2,3}")]
        public string email { get; set; }
    }
}
