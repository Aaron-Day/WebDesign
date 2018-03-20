using System;
using System.ComponentModel.DataAnnotations;

namespace BankLedger.Models
{
    public class BankViewModel
    {
        public BankViewModel()
        {
            Id = Guid.NewGuid().ToString("D");
            Balance = 0.00;
        }

        public string Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0.00, double.MaxValue, ErrorMessage = "Must be a positive amount")]
        public double Balance { get; set; }

        public string UserId { get; set; }
    }
}