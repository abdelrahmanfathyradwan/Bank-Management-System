using System;

namespace BankManagementSystem.Models
{
    internal class Transaction
    {
        public int Id { get; }
        public TransactionType Type { get; }
        public decimal Amount { get; }
        public decimal BalanceBeforeOperation { get; }
        public decimal BalanceAfterOperation { get; }
        public DateTime Date { get; }

        public int? RelatedAccountNumber { get; }
        public Guid? TransferId { get; }

        public Transaction(
            int id,
            TransactionType type,
            decimal amount,
            decimal balanceBeforeOperation,
            decimal balanceAfterOperation,
            DateTime date,
            int? relatedAccountNumber = null,
            Guid? transferId = null)
        {
            Id = id;
            Type = type;
            Amount = amount;
            BalanceBeforeOperation = balanceBeforeOperation;
            BalanceAfterOperation = balanceAfterOperation;
            Date = date;
            RelatedAccountNumber = relatedAccountNumber;
            TransferId = transferId;
        }
    }
}