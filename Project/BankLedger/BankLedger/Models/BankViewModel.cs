using System.Collections.Generic;

namespace BankLedger.Models
{
    public class BankViewModel
    {
        public string Id { get; set; }

        public BankAccount.AccountType Type { get; set; }

        public string Name { get; set; }

        private string UserId { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}