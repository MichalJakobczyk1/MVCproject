using System.ComponentModel.DataAnnotations;

namespace MVCproject.ViewModels
{
    public class EditEmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }
}
