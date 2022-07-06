using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.EntitiesProvider.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The Phone is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The UserType is required.")]
        public string UserType { get; set; }

        [Required(ErrorMessage = "The money is required.")]
        public decimal Money { get; set; }
    }
}
