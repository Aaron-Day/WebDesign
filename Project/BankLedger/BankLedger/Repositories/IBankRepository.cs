using BankLedger.Models;
using System.Collections.Generic;

namespace BankLedger.Repositories
{
    public interface IBankRepository
    {
        IEnumerable<BankAccount> GetAccounts(string userId);
        IEnumerable<Transaction> GetTransactions(string accountId);
    }
}