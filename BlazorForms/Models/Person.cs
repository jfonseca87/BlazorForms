using BlazorForms.Utils.CustomValidations;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorForms.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime Birthdate { get; set; }
        public bool IsActive { get; set; }
        public string Comments { get; set; }
        [CeroValidation(ErrorMessage = "Working Experience is required")]
        public int WorkingExperience { get; set; }
        [CeroValidation(ErrorMessage = "Working Abroad is required")]
        public int WorkingAbroad { get; set; }
        [DependentValidation(
            "WorkingExperience",
            "1",
            ErrorMessage = "Seniority Level is required")]
        public int SeniorityLevel { get; set; }
    }
}
