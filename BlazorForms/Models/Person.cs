using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorForms.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        public bool IsActive { get; set; }
        public string Comments { get; set; }
        [Required]
        public bool WorkingExperience { get; set; }
        [Required]
        public bool WorkingAbroad { get; set; }
        [Required]
        public int LevelSeniority { get; set; }
    }
}
