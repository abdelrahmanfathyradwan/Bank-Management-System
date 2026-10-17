using BankManagementSystem.Bank;
using BankManagementSystem.Models;
using BankManagementSystem.Services;
using System;
using System.Collections.Generic;

namespace BankManagementSystem
{
    internal class AccountService
    {
        private readonly Dictionary<int, BankAccount> _accounts = new();
        private readonly CustomerService _customerService;

        private int _nextAccountId = 10001;
        private int _nextTransactionId = 1;

        public AccountService(CustomerService customerService)
        {
            _customerService = customerService
                ?? throw new ArgumentNullException(nameof(customerService));
        }

        public BankAccount CreateBankAccount(int customerId)
        {
            Customer customer = _customerService.GetCustomerById(customerId);

            BankAccount account = new(_nextAccountId, customer);

            _accounts.Add(account.AccountNumber, account);
            _nextAccountId++;

            return account;
        }

        public BankAccount GetAccountById(int accountNumber)
        {
            if (!_accounts.TryGetValue(accountNumber, out BankAccount? account))
            {
                throw new KeyNotFoundException(
                    $"Account with number {accountNumber} was not found.");
            }

            return account;
        }

        public IEnumerable<BankAccount> GetAllAccounts()
        {
            return _accounts.Values;
        }

        public void Deposit(int accountNumber, decimal amount)
        {
            BankAccount account = GetAccountById(accountNumber);

            account.ValidateDeposit(amount);

            decimal balanceBefore = account.Balance;

            account.ApplyDeposit(amount);

            decimal balanceAfter = account.Balance;

            Transaction transaction = new(
                _nextTransactionId++,
                TransactionType.Deposit,
                amount,
                balanceBefore,
                balanceAfter,
                DateTime.Now);

            account.AddTransaction(transaction);
        }

        public void Withdraw(int accountNumber, decimal amount)
        {
            BankAccount account = GetAccountById(accountNumber);

            account.ValidateWithdrawal(amount);

            decimal balanceBefore = account.Balance;

            account.ApplyWithdrawal(amount);

            decimal balanceAfter = account.Balance;

            Transaction transaction = new(
                _nextTransactionId++,
                TransactionType.Withdrawal,
                amount,
                balanceBefore,
                balanceAfter,
                DateTime.Now);

            account.AddTransaction(transaction);
        }

        public void Transfer(
            int senderAccountNumber,
            int receiverAccountNumber,
            decimal amount)
        {
            if (senderAccountNumber == receiverAccountNumber)
            {
                throw new InvalidOperationException(
                    "Sender and receiver accounts must be different.");
            }

            BankAccount sender = GetAccountById(senderAccountNumber);
            BankAccount receiver = GetAccountById(receiverAccountNumber);

            // Validate both accounts before changing either balance.
            sender.ValidateWithdrawal(amount);
            receiver.ValidateDeposit(amount);

            decimal senderBalanceBefore = sender.Balance;
            decimal receiverBalanceBefore = receiver.Balance;

            decimal senderBalanceAfter = senderBalanceBefore - amount;
            decimal receiverBalanceAfter = receiverBalanceBefore + amount;

            // Prepare the history entries before applying the transfer.
            Guid transferId = Guid.NewGuid();
            DateTime transferDate = DateTime.Now;

            Transaction senderTransaction = new(
                _nextTransactionId,
                TransactionType.TransferOut,
                amount,
                senderBalanceBefore,
                senderBalanceAfter,
                transferDate,
                receiverAccountNumber,
                transferId);

            Transaction receiverTransaction = new(
                _nextTransactionId + 1,
                TransactionType.TransferIn,
                amount,
                receiverBalanceBefore,
                receiverBalanceAfter,
                transferDate,
                senderAccountNumber,
                transferId);

            // Apply the balance changes only after validation succeeds.
            sender.ApplyWithdrawal(amount);
            receiver.ApplyDeposit(amount);

            sender.AddTransaction(senderTransaction);
            receiver.AddTransaction(receiverTransaction);

            _nextTransactionId += 2;
        }

        public void FreezeAccount(int accountNumber)
        {
            BankAccount account = GetAccountById(accountNumber);
            account.Freeze();
        }

        public void UnfreezeAccount(int accountNumber)
        {
            BankAccount account = GetAccountById(accountNumber);
            account.Unfreeze();
        }

        public void CloseAccount(int accountNumber)
        {
            BankAccount account = GetAccountById(accountNumber);
            account.Close();
        }
    }
}