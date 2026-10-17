using BankManagementSystem.Bank;
using BankManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Presentation
{
    internal class CustomerMenu
    {
        public static void CreateCustomer(CustomerService customerService)
        {
            Console.Write("Enter Full Name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter Phone Number: ");
            string phone = Console.ReadLine() ?? string.Empty;

            try
            {
                Customer customer =
                    customerService.CreateCustomer(name, phone);

                Console.WriteLine(
                    $"Customer created successfully. ID: {customer.Id}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
