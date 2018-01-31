using System;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Data.Entities
{
    public class User
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        //int required by default
        [Display(Name = "Years in School")]
        public int YearsInSchool { get; set; }

        //DateTime required by default
        [Display(Name = "DOB")]
        public DateTime DateOfBirth { get; set; }

        //int required by default
        [Display(Name = "Age")]
        public int Age { get; set; }
    }
}