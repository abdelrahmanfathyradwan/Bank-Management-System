using BankManagementSystem.Bank;

namespace BankManagementSystem.Services
{
    internal class CustomerService
    {
        private readonly Dictionary<int, Customer> _customers = new();

        private int _nextCustomerId = 1;

        public Customer GetCustomerById(int customerId)
        {
            if (!_customers.TryGetValue(customerId, out Customer? customer))
            {
                throw new KeyNotFoundException(
                    $"Customer with ID {customerId} was not found.");
            }

            return customer;
        }

        public Customer CreateCustomer(string name, string phone)
        {

            if (string.IsNullOrWhiteSpace(name))           
            {
                throw new ArgumentException(
                    "Customer name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException(
                    "Phone number cannot be empty.");
            }

            Customer customer = new(
                _nextCustomerId,
                name.Trim(),
                phone.Trim());

            _customers.Add(customer.Id, customer);

            _nextCustomerId++;

            return customer;
        }
    }
}

