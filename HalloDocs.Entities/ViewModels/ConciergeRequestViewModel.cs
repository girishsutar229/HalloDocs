﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalloDocs.Entities.ViewModels
{
    public class ConciergeRequestViewModel
    {

        [Key]
        public int UserId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Please Enter a valid First Name")]
        public string FirstName { get; set; } = null!;

        [StringLength(100)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Email ID")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}", ErrorMessage = "Email is not valid.")]
        [StringLength(50)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter PhoneNumber")]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        public string? ContryCode { get; set; }

        [StringLength(100)]
        public string? PropertyName { get; set; }

        [Required(ErrorMessage = "Please Enter a Street")]
        [StringLength(100)]
        public string Street { get; set; } = null!;


        [Required(ErrorMessage = "Please Enter City")]
        [StringLength(100)]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Please EnterState")]
        [StringLength(100)]
        public string State { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a Zip Code")]
        [RegularExpression(@"^(?!\d{7}$)\d{6}$", ErrorMessage = "Zip Code must be exactly 6 digits long")]
        public string ZipCode { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter First")]
        [StringLength(100)]
        public string PatientFirstName { get; set; } = null!;

        [StringLength(100)]
        public string? PatientLastName { get; set; }

        [Required(ErrorMessage = "Please Enter Patient Email ID")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}", ErrorMessage = "email is not valid.")]
        [StringLength(50)]
        public string PatientEmail { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter PatientPhoneNumber")]
        [StringLength(20)]
        public string? PatientPhoneNumber { get; set; }


        public string? PatientContryCode { get; set; }

        [StringLength(10)]
        public string? PatientRoomNumber { get; set; }

        [Required(ErrorMessage = "Please provide a Birth Date")]
        [DataType(DataType.Date)]
        public DateOnly PatientDateOfBirth { get; set; }

        [StringLength(500)]
        public string? PatientSymptoms { get; set; }
    }
}
