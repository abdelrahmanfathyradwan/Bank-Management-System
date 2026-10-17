using System;
using System.Collections.Generic;
using BankManagementSystem.Models;

namespace BankManagementSystem.Bank
{
    internal class BankAccount
    {
        private readonly List<Transaction> _history = new();

        public int AccountNumber { get; private set; }

        public decimal Balance { get; private set; }

        public AccountStatus Status { get; private set; }
            = AccountStatus.Active;

        public AccountType AccountType { get; private set; }
            = AccountType.Current;

        public Customer Customer { get; }

        public IReadOnlyList<Transaction> History => _history.AsReadOnly();

        public BankAccount(int accountNumber, Customer customer)
        {
            AccountNumber = accountNumber;
            Customer = customer
                ?? throw new ArgumentNullException(nameof(customer));
        }

        internal void ValidateDeposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    "Deposit amount must be greater than zero.");
            }

            if (Status != AccountStatus.Active)
            {
                throw new InvalidOperationException(
                    "Cannot deposit into an account that is not active.");
            }

            if (Balance > decimal.MaxValue - amount)
            {
                throw new OverflowException(
                    "The resulting balance exceeds the supported limit.");
            }
        }

        internal void ValidateWithdrawal(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    "Withdrawal amount must be greater than zero.");
            }

            if (Status != AccountStatus.Active)
            {
                throw new InvalidOperationException(
                    "Cannot withdraw from an account that is not active.");
            }

            if (amount > Balance)
            {
                throw new InvalidOperationException(
                    "Insufficient balance.");
            }
        }

        internal void ApplyDeposit(decimal amount)
        {
            Balance += amount;
        }

        internal void ApplyWithdrawal(decimal amount)
        {
            Balance -= amount;
        }

        internal void AddTransaction(Transaction transaction)
        {
            _history.Add(transaction);
        }

        public void Freeze()
        {
            if (Status != AccountStatus.Active)
            {
                throw new InvalidOperationException(
                    "Only active accounts can be frozen.");
            }

            Status = AccountStatus.Frozen;
        }

        public void Unfreeze()
        {
            if (Status != AccountStatus.Frozen)
            {
                throw new InvalidOperationException(
                    "Only frozen accounts can be unfrozen.");
            }

            Status = AccountStatus.Active;
        }

        public void Close()
        {
            if (Status == AccountStatus.Closed)
            {
                throw new InvalidOperationException(
                    "Account is already closed.");
            }

            Status = AccountStatus.Closed;
        }
    }
}