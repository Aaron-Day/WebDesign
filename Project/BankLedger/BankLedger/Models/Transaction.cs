using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankLedger.Models
{
    public class Transaction
    {
        public enum TransactionType { Withdrawl, Deposit };

        [Key]
        public string Id { get; set; }

        public string TimeStamp { get; set; }

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        [ForeignKey("BankAccount")]
        private string BankAccountId { get; set; }
        private BankAccount BankAccount { get; set; }
    }
}