using BankLedger.Models;
using System.Collections.Generic;

namespace BankLedger.Services
{
    public interface IBankService
    {
        BankViewModel GetAccount(string id);
        IEnumerable<BankViewModel> GetAccounts(string userId);
        IEnumerable<Transaction> GetTransactions(string accountId);

        void Create(BankViewModel account);
        void Update(BankViewModel account);
        void Delete(string id);

        void Deposit(string account, double amount);
        void Withdrawl(string account, double amount);
    }
}