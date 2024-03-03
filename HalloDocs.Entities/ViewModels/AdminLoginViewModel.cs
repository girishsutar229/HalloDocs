using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class AdminLoginViewModel
    {
        [Key]

        [Required(ErrorMessage = "Please enter the email")]
        [StringLength(256)]
        public string Email { get; set; } = null!;

        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Please the EnterValid password")]
        [Required(ErrorMessage = "Password is required")]
        [Column(TypeName = "character varying")]
        [DataType(DataType.Password)]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = null!;
    }
}
