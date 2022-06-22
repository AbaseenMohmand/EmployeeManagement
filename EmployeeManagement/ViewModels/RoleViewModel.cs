using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
