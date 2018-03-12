using BankLedger.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BankLedger.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly ApplicationDbContext _context;

        public BankRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BankAccount> GetAccounts(string userId)
        {
            var accounts = _context.BankAccounts.Where(b => b.UserId == userId)
                .Include(c => c.Transactions)
                .AsNoTracking();
            return accounts.ToList();
        }

        public IEnumerable<Transaction> GetTransactions(string accountId)
        {
            throw new NotImplementedException();
        }
    }
}