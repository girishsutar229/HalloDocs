using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class PatientResetePasswordViewModel
    {
        [Required(ErrorMessage = "Please enter the email")]
        [StringLength(256)]
        public string Email { get; set; } = null!;
    }
}
