using System;

namespace BlazorFormsAPI.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsActive { get; set; }
        public string Comments { get; set; }
        public bool WorkingExperience { get; set; }
        public bool WorkingAbroad { get; set; }
        public int SeniorityLevel { get; set; }
    }
}
