using BankManagementSystem.Bank;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Presentation
{
    internal class AccountMenu
    {
        public static void CreateBankAccount(AccountService accountService)
        {
            Console.Write("Enter Customer ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            try
            {
                BankAccount account = accountService.CreateBankAccount(id);

                Console.WriteLine($"Account added successfully!. ID = {account.AccountNumber}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ViewAccount(AccountService accountService)
        {
            Console.WriteLine("Enter Your Account Number: ");

            if (!int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                Console.WriteLine("Invalid Input");
                return;
            }

            try
            {
                BankAccount account = accountService.GetAccountById(accountNumber);

                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Customer Name: {account.Customer.Name}");
                Console.WriteLine($"Account Status: {account.Status}");
                Console.WriteLine($"Account Type: {account.AccountType}");
                Console.WriteLine($"Balance: {account.Balance}");
                Console.WriteLine($"Customer Phone: {account.Customer.Phone}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static void ViewAllAccounts(AccountService accountService)
        {
            var accounts = accountService.GetAllAccounts();

            if (!accounts.Any())
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("There Are Not Found Any Account");
                Console.WriteLine("--------------------------------");
                return;
            }

            foreach (var account in accounts)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Account Status: {account.Status}");
                Console.WriteLine($"Account Type: {account.AccountType}");
                Console.WriteLine($"Customer Name: {account.Customer.Name}");
                Console.WriteLine($"Balance: {account.Balance}");
                Console.WriteLine($"Customer Phone: {account.Customer.Phone}");
                Console.WriteLine("--------------------------------");
            }
        }

        public static void FreezeAccount(AccountService accountService)
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
                accountService.FreezeAccount(accountNumber);

                Console.WriteLine($"Account {account.AccountNumber} belongs to {account.Customer.Name} and has status {account.Status}.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void UnfreezeAccount(AccountService accountService)
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
                accountService.UnfreezeAccount(accountNumber);

                Console.WriteLine($"Account {account.AccountNumber} belongs to {account.Customer.Name} and has status {account.Status}.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void CloseAccount(AccountService accountService)
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
                accountService.CloseAccount(accountNumber);

                Console.WriteLine($"Account {account.AccountNumber} belongs to {account.Customer.Name} and has status {account.Status}.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
