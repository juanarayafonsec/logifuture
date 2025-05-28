using System;

namespace WalletService.Business.Entities
{
    public class Wallet
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public string Currency { get; private set; }
        public decimal Balance { get; private set; }

        protected Wallet() { }

        public Wallet(Guid id, Guid customerId, string currency)
        {
            if (!Enum.TryParse<CurrencyType>(currency.ToUpper(), out var parsedCurrency))
            {
                throw new ArgumentException($"Invalid currency type: {currency}");
            }

            Id = id;
            CustomerId = customerId;
            Currency = currency.ToUpper();
            Balance = 0;
        }

        public void AddFunds(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            Balance += amount;
        }

        public void SubtractFunds(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (Balance < amount) throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
        }
    }
}