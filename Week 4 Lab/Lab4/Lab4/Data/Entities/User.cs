using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab4.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        // DateTime required by default
        [DataType(DataType.Date)]
        [Display(Name = "DOB")]
        public DateTime DateOfBirth { get; set; }

        // int required by default
        [Display(Name = "Years in School")]
        public int YearsInSchool { get; set; }

        public IEnumerable<Pet> Pets { get; set; }
    }
}