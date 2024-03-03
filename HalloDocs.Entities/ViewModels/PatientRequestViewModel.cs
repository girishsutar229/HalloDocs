﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HalloDocs.Entities.ViewModels
{
    public class PatientRequestViewModel
    {

        [Key]
        public string? Symptoms { get; set; }

        [Required(ErrorMessage = "Please Enter FirstName")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter FirstName")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Email ID")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}", ErrorMessage = "Email is not valid.")]
        //[EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(50)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter PhoneNumber")]
        [StringLength(23)]
        public string? PhoneNumber { get; set; }

        public string? ContryCode { get; set; }

        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Please the EnterValid password")]
      
        [DataType(DataType.Password)]
        [MaxLength(255)] // Adjust the max length as per your hashing algorithm
        public string? PasswordHash { get; set; }

        
        [Compare("PasswordHash", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [NotMapped] // This property won't be mapped to the database
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter a Street name")]
        [StringLength(100)]
        public string? Street { get; set; }

        [Required(ErrorMessage = "Please enter a City name")]
        [StringLength(100)]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a State name")]
        [StringLength(100)]
        public string State { get; set; } = null!;

     
        [Required(ErrorMessage = "Please enter a Zip Code")]
        [RegularExpression(@"^(?!\d{7}$)\d{6}$", ErrorMessage = "Zip Code must be exactly 6 digits long")]
        public string ZipCode { get; set; } = null!;

        [Required(ErrorMessage = "Please provide a Birth Date")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [StringLength(50)]
        public string? PatientRoomNumber { get; set; }


        //public IFormFile formFile { get; set; }

        public List<IFormFile>? formFile { get; set; }

        public string? Longitude { get; set; }

        public string? Latitude { get; set; }
    }
}
