using BankLedger.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankLedger.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILog _log = LogManager.GetLogger(typeof(BankRepository));

        public BankRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public BankAccount GetAccount(string id)
        {
            try
            {
                _log.Info($"GetAccount: {id}");
                return _context.BankAccounts.Find(id);
            }
            catch (Exception e)
            {
                _log.Error("Error getting account: ", e);
                throw;
            }
        }

        public IEnumerable<BankAccount> GetAccounts(string userId)
        {
            _log.Info($"GetAccounta: {userId}");
            return _context.BankAccounts.Where(b => b.UserId == userId).ToList();
        }

        public IEnumerable<Transaction> GetTransactions(string accountId)
        {
            _log.Info($"GetTransactions: {accountId}");
            return _context.Transactions.Where(T => T.BankAccountId == accountId).OrderByDescending(t => t.Id);
        }

        public void Create(BankAccount account)
        {
            try
            {
                _context.BankAccounts.Add(account);
                _context.Transactions.Add(new Transaction
                {
                    BankAccountId = account.Id,
                    Type = "New Account",
                    TimeStamp = $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}",
                    Amount = 0.00,
                    Balance = account.Balance
                });
                _context.SaveChanges();
                _log.Info($"Create: {account.Id}");
            }
            catch (Exception e)
            {
                _log.Error("Error creating account: ", e);
                throw;
            }
        }

        public void Update(BankAccount account)
        {
            try
            {
                _context.SaveChanges();
                _log.Info($"Update: {account.Id}");
            }
            catch (Exception e)
            {
                _log.Error("Error updating account: ", e);
                throw;
            }
        }

        public void Delete(string id)
        {
            try
            {
                _context.BankAccounts.Remove(_context.BankAccounts.Find(id) ?? throw new InvalidOperationException());
                _context.SaveChanges();
                _log.Info($"Delete: {id}");
            }
            catch (Exception e)
            {
                _log.Error("Error deleting account: ", e);
                throw;
            }
        }

        public void Deposit(string account, double amount)
        {
            try
            {
                var acct = _context.BankAccounts.Find(account);
                if (acct == null) return;
                acct.Balance += amount;
                _context.Transactions.Add(new Transaction
                {
                    BankAccountId = acct.Id,
                    Type = "Deposit",
                    TimeStamp = $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}",
                    Amount = amount,
                    Balance = acct.Balance
                });
                _context.SaveChanges();
                _log.Info($"Deposit: {account} ${amount}");
            }
            catch (Exception e)
            {
                _log.Error("Error depositing to account: ", e);
                throw;
            }
        }

        public void Withdrawl(string account, double amount)
        {
            try
            {
                var acct = _context.BankAccounts.Find(account);
                if (acct == null || !(acct.Balance >= amount)) return;
                acct.Balance -= amount;
                _context.Transactions.Add(new Transaction
                {
                    BankAccountId = acct.Id,
                    Type = "Withdrawl",
                    TimeStamp = $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}",
                    Amount = amount,
                    Balance = acct.Balance
                });
                _context.SaveChanges();
                _log.Info($"Withdrawl: {account} ${amount}");
            }
            catch (Exception e)
            {
                _log.Error("Error withdrawing from account: ", e);
                throw;
            }
        }
    }
}