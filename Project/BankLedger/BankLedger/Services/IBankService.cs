using BankLedger.Models;
using System.Collections.Generic;

namespace BankLedger.Services
{
    public interface IBankService
    {
        IEnumerable<BankAccount> GetAccounts(string userId);
        IEnumerable<Transaction> GetTransactions(string accountId);
    }
}