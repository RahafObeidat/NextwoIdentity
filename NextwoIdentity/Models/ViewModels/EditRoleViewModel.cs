using System.ComponentModel.DataAnnotations;

namespace Next2Identity.Models.ViewModels
{
    public class EditRoleViewModel
    {

        public EditRoleViewModel()
        {
            Users = new List<string>();
        }

        public string? RoleId { get; set; }
        [Required(ErrorMessage = "role name is required")]
        public string? RoleName { get; set; }

        public List<string>? Users { get; set; }
    }
}