using System;
using System.Threading.Tasks;
using WalletService.Api.Business.Interfaces;
using WalletService.Business.Entities;
using WalletService.Business.Interfaces;

namespace WalletService.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateWalletAsync(Guid customerId, string currency)
        {
            if (await _unitOfWork.Wallets.ExistsByCustomerAndCurrencyAsync(customerId, currency))
                throw new InvalidOperationException("A wallet with this currency already exists for the customer.");

            var wallet = new Wallet(Guid.NewGuid(), customerId, currency);
            await _unitOfWork.Wallets.AddAsync(wallet);
            await _unitOfWork.SaveChangesAsync();
            return wallet.Id;
        }

        public async Task DepositAsync(Guid walletId, Guid transactionId, decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (await _unitOfWork.Transactions.ExistsAsync(transactionId)) throw new InvalidOperationException($"Transaction with ID {transactionId} already exists"); ;
            var wallet = await _unitOfWork.Wallets.GetByIdAsync(walletId)
                                      ?? throw new InvalidOperationException("Wallet not found");
            wallet.AddFunds(amount);

            await _unitOfWork.Transactions.AddAsync(new WalletTransaction
            {
                Id = transactionId,
                WalletId = walletId,
                Amount = amount,
                Timestamp = DateTime.UtcNow,
                Type = TransactionType.Deposit.ToString()
            });

            _unitOfWork.Wallets.Update(wallet);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task WithdrawAsync(Guid walletId, Guid transactionId, decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (await _unitOfWork.Transactions.ExistsAsync(transactionId)) throw new InvalidOperationException($"Transaction with ID {transactionId} already exists"); ;

            var wallet = await _unitOfWork.Wallets.GetByIdAsync(walletId)
                         ?? throw new InvalidOperationException("Wallet not found");
            wallet.SubtractFunds(amount);

            await _unitOfWork.Transactions.AddAsync(new WalletTransaction
            {
                Id = transactionId,
                WalletId = walletId,
                Amount = amount,
                Timestamp = DateTime.UtcNow,
                Type = TransactionType.Withdraw.ToString()
            });

            _unitOfWork.Wallets.Update(wallet);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<decimal> GetBalanceAsync(Guid walletId)
        {
            var wallet = await _unitOfWork.Wallets.GetByIdAsync(walletId)
                          ?? throw new InvalidOperationException("Wallet not found");

            return wallet.Balance;
        }
    }
}