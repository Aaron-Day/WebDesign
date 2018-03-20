using BankLedger.Models;
using System.Collections.Generic;

namespace BankLedger.Repositories
{
    public interface IBankRepository
    {
        BankAccount GetAccount(string id);
        IEnumerable<BankAccount> GetAccounts(string userId);
        IEnumerable<Transaction> GetTransactions(string accountId);

        void Create(BankAccount account);
        void Update(BankAccount account);
        void Delete(string id);

        void Deposit(string account, double amount);
        void Withdrawl(string account, double amount);
    }
}