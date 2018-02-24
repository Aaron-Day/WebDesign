using System;
using System.ComponentModel.DataAnnotations;

namespace Lab7.Data.Entities
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Pet's Name")]
        public string Name { get; set; }

        [Display(Name = "Pet's Age")]
        public int Age { get; set; }

        [Display(Name = "Next Checkup")]
        public DateTime NextCheckup { get; set; }

        [Required]
        [Display(Name = "Vet's Name")]
        public string VetName { get; set; }

        [Display(Name = "Owner's ID")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}