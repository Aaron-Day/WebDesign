using BankLedger.Models;
using BankLedger.Repositories;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankLedger.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _repository;
        private readonly ILog _log = LogManager.GetLogger(typeof(BankService));

        public BankService(IBankRepository repository)
        {
            _repository = repository;
        }

        public BankViewModel GetAccount(string id)
        {
            return MapToBankViewModel(_repository.GetAccount(id));
        }

        public IEnumerable<BankViewModel> GetAccounts(string userId)
        {
            var accounts = _repository.GetAccounts(userId);
            return accounts.Select(MapToBankViewModel).ToList();
        }

        public IEnumerable<Transaction> GetTransactions(string accountId)
        {
            return _repository.GetTransactions(accountId).ToList();
        }

        public void Create(BankViewModel account)
        {
            try
            {
                _repository.Create(MapToBankAccount(account));
            }
            catch (Exception e)
            {
                _log.Error("Error creating account: ", e);
                throw;
            }
        }

        public void Update(BankViewModel account)
        {
            try
            {
                var acct = _repository.GetAccount(account.Id);
                CopyToAccount(account, acct);
                _repository.Update(acct);
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
                _repository.Delete(id);
            }
            catch (Exception e)
            {
                _log.Error("Error deleting account: ", e);
                throw;
            }
        }

        public void Deposit(string account, double amount)
        {
            _repository.Deposit(account, amount);
        }

        public void Withdrawl(string account, double amount)
        {
            _repository.Withdrawl(account, amount);
        }



        private BankAccount MapToBankAccount(BankViewModel account)
        {
            try
            {
                return new BankAccount
                {
                    Id = account.Id,
                    Name = account.Name,
                    Type = account.Type,
                    Balance = account.Balance,
                    UserId = account.UserId
                };
            }
            catch (Exception e)
            {
                _log.Error("Error mapping to bank account: ", e);
                throw;
            }
        }

        private BankViewModel MapToBankViewModel(BankAccount account)
        {
            try
            {
                return new BankViewModel
                {
                    Id = account.Id,
                    Name = account.Name,
                    Type = account.Type,
                    Balance = account.Balance,
                    UserId = account.UserId
                };
            }
            catch (Exception e)
            {
                _log.Error("Error mapping to bank view model: ", e);
                throw;
            }
        }

        private void CopyToAccount(BankViewModel view, BankAccount account)
        {
            try
            {
                account.Id = view.Id;
                account.Name = view.Name;
                account.Type = view.Type;
                account.Balance = view.Balance;
                account.UserId = view.UserId;
            }
            catch (Exception e)
            {
                _log.Error("Error copying to account: ", e);
                throw;
            }
        }
    }
}