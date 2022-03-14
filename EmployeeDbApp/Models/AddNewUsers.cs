using System.ComponentModel.DataAnnotations;


namespace EmployeeDbApp.Models
{
    public class AddNewUsers
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        
        public string Roles { get; set; }

    }
}
