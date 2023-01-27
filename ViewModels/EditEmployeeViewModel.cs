using MVCproject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.ViewModels
{
    public class EditEmployeeViewModel
    {
        public int Id { get; set; }
        public DateTime HiredOn { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address? Address { get; set; }

        [ForeignKey("Info")]
        public int InfoId { get; set; }
        public Info? Info { get; set; }
    }
}
