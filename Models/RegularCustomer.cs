using System.ComponentModel.DataAnnotations;

namespace MVCproject.Models
{
    public class RegularCustomer
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime SinceWhen { get; set; }
        public string Nip { get; set; } 
    }
}
