using BankManagementSystem.Bank;
using BankManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankManagementSystem.Presentation
{
    internal class TransactionMenu
    {
        public static void Deposit(AccountService accountService)
        {
            Console.Write("Enter Account Number: ");

            if (!int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                Console.WriteLine("Invalid account number.");
                return;
            }

            Console.Write("Enter Amount: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            try
            {
                BankAccount account = accountService.GetAccountById(accountNumber);

                account.ApplyDeposit(amount);

                Console.WriteLine("Amount deposited successfully.");
                Console.WriteLine($"New Balance: {account.Balance}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Invalid amount: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Deposit failed: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void Withdraw(AccountService accountService)
        {
            Console.Write("Enter Account Number: ");

            if (!int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                Console.WriteLine("Invalid account number.");
                return;
            }

            Console.Write("Enter Amount: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            try
            {
                BankAccount account = accountService.GetAccountById(accountNumber);

                account.ApplyWithdrawal(amount);

                Console.WriteLine("Amount Withdraw successfully.");
                Console.WriteLine($"New Balance: {account.Balance}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Invalid amount: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Deposit failed: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void Transfer(AccountService accountService)
        {
            Console.Write("Enter Sender Account Number: ");

            if (!int.TryParse(Console.ReadLine(), out int senderAccountNumber))
            {
                Console.WriteLine("Invalid account number.");
                return;
            }

            Console.Write("Enter Reciver Account Number: ");

            if (!int.TryParse(Console.ReadLine(), out int reciverAccountNumber))
            {
                Console.WriteLine("Invalid account number.");
                return;
            }

            Console.Write("Enter Amount: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            try
            {
                BankAccount senderAccount = accountService.GetAccountById(senderAccountNumber);
                BankAccount reciverAccount = accountService.GetAccountById(reciverAccountNumber);

                Console.WriteLine($"Balance of {senderAccount.Customer.Name} before transfer operation is: {senderAccount.Balance}");
                Console.WriteLine($"Balance of {reciverAccount.Customer.Name} before transfer operation is: {reciverAccount.Balance}");
                Console.WriteLine($"Amount of transfer operation is: {amount}");
                Console.WriteLine($"----------------------------------------------------------------------------");
                accountService.Transfer(senderAccountNumber, reciverAccountNumber, amount);
                Console.WriteLine($"Balance of {senderAccount.Customer.Name} after transfer operation is: {senderAccount.Balance}");
                Console.WriteLine($"Balance of {reciverAccount.Customer.Name} after transfer operation is: {reciverAccount.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void TransactionHistory(AccountService accountService)
        {
            Console.Write("Enter Account Number: ");

            if (!int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                Console.WriteLine("Invalid account number.");
                return;
            }

            try
            {
                BankAccount account = accountService.GetAccountById(accountNumber);

                Console.WriteLine("\n========== TRANSACTION HISTORY ==========");
                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Customer: {account.Customer.Name}");
                Console.WriteLine($"Status: {account.Status}");

                Console.WriteLine();
                Console.WriteLine(
                    $"{"ID",-5} {"Type",-12} {"Amount",12} {"Before",12} {"After",12} {"Date",-20}");

                Console.WriteLine(new string('-', 90));

                foreach (Transaction operation in account.History.OrderByDescending(t => t.Date))
                {
                    Console.WriteLine(
                        $"{operation.Id,-5} " +
                        $"{operation.Type,-12} " +
                        $"{operation.Amount,12:N2} " +
                        $"{operation.BalanceBeforeOperation,12:N2} " +
                        $"{operation.BalanceAfterOperation,12:N2} " +
                        $"{operation.Date,-20:yyyy-MM-dd HH:mm:ss}");

                    if (operation.RelatedAccountNumber.HasValue)
                    {
                        Console.WriteLine(
                            $"      Related Account: {operation.RelatedAccountNumber}");
                    }
                }

                Console.WriteLine(new string('-', 90));
                Console.WriteLine($"Total Transactions: {account.History.Count}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
