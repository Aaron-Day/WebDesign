using BankLedger.Models;
using BankLedger.Repositories;
using System;
using System.Collections.Generic;

namespace BankLedger.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _repository;

        public BankService(IBankRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BankAccount> GetAccounts(string userId)
        {
            var accounts = _repository.GetAccounts(userId);
            return accounts;
        }

        public IEnumerable<Transaction> GetTransactions(string accountId)
        {
            throw new NotImplementedException();
        }
    }
}