using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class PatientUpdatePasswordViewModel
    {
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Please the EnterValid password")]
        [Required(ErrorMessage = "Password is required")]
        [Column(TypeName = "character varying")]
        [DataType(DataType.Password)]
        [MaxLength(255)]
        public string? PasswordHash { get; set; } = null!;

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("PasswordHash", ErrorMessage = "Confrimpassword doesn't match")]
        [DataType(DataType.Password)]
        [NotMapped] // This property won't be mapped to the database
        public string ConfirmPassword { get; set; } = null!;
    }
}
