using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankLedger.Models
{
    public class BankAccount
    {
        public BankAccount()
        {
            Transactions = new List<Transaction>();
        }

        [Key]
        public string Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}