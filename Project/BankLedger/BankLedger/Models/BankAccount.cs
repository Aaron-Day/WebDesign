using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankLedger.Models
{
    public class BankAccount
    {
        public enum AccountType { Checking, Savings }

        public BankAccount()
        {
            Transactions = new List<Transaction>();
        }

        [Key]
        public string Id { get; set; }

        public AccountType Type { get; set; }

        public string Name { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        private ApplicationUser User { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}