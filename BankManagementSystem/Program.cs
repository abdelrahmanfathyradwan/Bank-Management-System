
using BankManagementSystem.Services;

using BankManagementSystem.Presentation;


namespace BankManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomerService customerService = new();
            AccountService accountService = new(customerService);

            while (true)
            {
                DisplayMenu();

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        CustomerMenu.CreateCustomer(customerService);
                        break;

                    case 2:
                        AccountMenu.CreateBankAccount(accountService);
                        break;

                    case 3:
                        AccountMenu.ViewAccount(accountService);
                        break;

                    case 4:
                        AccountMenu.ViewAllAccounts(accountService);
                        break;

                    case 5:
                        TransactionMenu.Deposit(accountService);
                        break;

                    case 6:
                        TransactionMenu.Withdraw(accountService);
                        break;

                    case 7:
                        TransactionMenu.Transfer(accountService);
                        break;

                    case 8:
                        AccountMenu.FreezeAccount(accountService);
                        break;

                    case 9:
                        AccountMenu.UnfreezeAccount(accountService);
                        break;

                    case 10:
                        AccountMenu.CloseAccount(accountService);
                        break;

                    case 11:
                        TransactionMenu.TransactionHistory(accountService);
                        break;
                    case 0:
                        Console.WriteLine("Program ended.");
                        return;

                    default:
                        Console.WriteLine("Invalid menu choice.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine(
                "=================================\n" +
                "     BANK MANAGEMENT SYSTEM\n" +
                "=================================\n\n" +
                "1. Create Customer\n" +
                "2. Create Bank Account\n" +
                "3. View Account\n" +
                "4. View All Accounts\n" +
                "5. Deposit\n" +
                "6. Withdraw\n" +
                "7. Transfer\n" +
                "8. Freeze Account\n" +
                "9. Unfreeze Account\n" +
                "10. Close Account\n" +
                "11. Transaction History\n" +
                "12. Bank Statistics\n" +
                "0. Exit\n\n" +
                "Select an option:");
        }
    }
}
