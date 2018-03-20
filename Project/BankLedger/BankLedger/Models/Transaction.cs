using System;
using System.ComponentModel.DataAnnotations;

namespace BankLedger.Models
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        public string TimeStamp { get; set; }

        public string Type { get; set; }

        public double Amount { get; set; }

        public double Balance { get; set; }

        public string BankAccountId { get; set; }
        private BankAccount BankAccount { get; set; }
    }
}