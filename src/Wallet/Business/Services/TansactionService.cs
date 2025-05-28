using System;
using System.Threading.Tasks;
using WalletService.Api.Business.Interfaces;
using WalletService.Business.Entities;
using WalletService.Business.Interfaces;

namespace WalletService.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletTransactionRepository _transactionRepository;

        public TransactionService(IWalletRepository walletRepository, IWalletTransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Guid> CreateWalletAsync(Guid customerId, string currency)
        {
            var wallet = new Wallet(Guid.NewGuid(), customerId, currency);
            await _walletRepository.AddAsync(wallet);
            await _walletRepository.SaveChangesAsync();
            return wallet.Id;
        }

        public async Task DepositAsync(Guid walletId, Guid transactionId, decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (await _transactionRepository.ExistsAsync(transactionId)) throw new InvalidOperationException($"Transaction with ID {transactionId} already exists"); ;

            var wallet = await _walletRepository.GetByIdAsync(walletId) ?? throw new InvalidOperationException("Wallet not found");
            wallet.AddFunds(amount);

            await _transactionRepository.AddAsync(new WalletTransaction
            {
                Id = transactionId,
                WalletId = walletId,
                Amount = amount,
                Timestamp = DateTime.UtcNow,
                Type = TransactionType.Deposit.ToString()
            });

            _walletRepository.Update(wallet);
            await _walletRepository.SaveChangesAsync();
            await _transactionRepository.SaveChangesAsync();
        }

        public async Task WithdrawAsync(Guid walletId, Guid transactionId, decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (await _transactionRepository.ExistsAsync(transactionId)) throw new InvalidOperationException($"Transaction with ID {transactionId} already exists"); ;

            var wallet = await _walletRepository.GetByIdAsync(walletId) ?? throw new InvalidOperationException("Wallet not found");
            wallet.SubtractFunds(amount);

            await _transactionRepository.AddAsync(new WalletTransaction
            {
                Id = transactionId,
                WalletId = walletId,
                Amount = amount,
                Timestamp = DateTime.UtcNow,
                Type = TransactionType.Withdraw.ToString()
            });

            _walletRepository.Update(wallet);
            await _walletRepository.SaveChangesAsync();
            await _transactionRepository.SaveChangesAsync();
        }

        public async Task<decimal> GetBalanceAsync(Guid walletId)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletId) ?? throw new InvalidOperationException("Wallet not found");
            return wallet.Balance;
        }
    }
}