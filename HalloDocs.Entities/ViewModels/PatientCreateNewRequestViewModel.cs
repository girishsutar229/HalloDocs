using HalloDocs.Entities.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class PatientCreateNewRequestViewModel
    {

        [Key]

        [StringLength(500)]
        public string? PatientSymptoms { get; set; }

        [Required(ErrorMessage = "Please Enter PatientFirstName")]
        [StringLength(100)]
        public string PatientFirstName { get; set; } = null!;

        [StringLength(100)]
        public string? PatientLastName { get; set; }

        [Required(ErrorMessage = "Please Enter PatientEmail")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}", ErrorMessage = "Email is not valid.")]
        [StringLength(50)]
        public string PatientEmail { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter  PatientPhonenumber")]
        public string? PatientPhoneNumber { get; set; }

        public string? PatientContryCode { get; set; }

        [Required(ErrorMessage = "Please Enter PatientStreet ")]
        [StringLength(100)]
        public string PatientStreet { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter  PatientCity")]
        [StringLength(100)]
        public string PatientCity { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter  PatientState")]
        [StringLength(100)]
        public string PatientState { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter  PatientZipCode")]
        [StringLength(10)]
        public string PatientZipCode { get; set; } = null!;

        public DateOnly PatientDateOfBirth { get; set; }

        [StringLength(10)]
        public string? PatientRoomNumber { get; set; }

        [StringLength(20)]
        public string? PatientRelationName { get; set; }

        public List<IFormFile>? formFile { get; set; }

        public List<User>? userList { get; set; } =new List<User>();

        public string? Longitude { get; set; }

        public string? Latitude { get; set; }

        public string? BirthDate { get; set; }
    }
}
