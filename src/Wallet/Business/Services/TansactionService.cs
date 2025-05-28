using System;
using System.Data.Entity;
using System.Threading.Tasks;
using WalletService.Business.Entities;
using WalletService.Business.Interfaces;
using WalletService.Data.Context;

namespace WalletService.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly WalletDbContext _context;
        public TransactionService(WalletDbContext context) => _context = context;

        public async Task<Guid> CreateWalletAsync(Guid customerId, string currency)
        {
            var wallet = new Wallet(Guid.NewGuid(), customerId, currency);
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
            return wallet.Id;
        }

        public async Task DepositAsync(Guid walletId, Guid transactionId, decimal amount)
        {
            if (await _context.WalletTransactions.AnyAsync(t => t.Id == transactionId)) return;
            var wallet = await _context.Wallets.FindAsync(walletId) ?? throw new Exception("Wallet not found");
            wallet.AddFunds(amount);
            _context.WalletTransactions.Add(new WalletTransaction
            {
                Id = transactionId,
                WalletId = walletId,
                Amount = amount,
                Timestamp = DateTime.UtcNow,
                Type = TransactionType.Deposit.ToString(),
            });
            await _context.SaveChangesAsync();
        }

        public async Task WithdrawAsync(Guid walletId, Guid transactionId, decimal amount)
        {
            if (await _context.WalletTransactions.AnyAsync(t => t.Id == transactionId)) return;
            var wallet = await _context.Wallets.FindAsync(walletId) ?? throw new Exception("Wallet not found");
            wallet.SubtractFunds(amount);
            _context.WalletTransactions.Add(new WalletTransaction
            {
                Id = transactionId,
                WalletId = walletId,
                Amount = amount,
                Timestamp = DateTime.UtcNow,
                Type = TransactionType.Withdraw.ToString()
            });
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetBalanceAsync(Guid walletId)
        {
            var wallet = await _context.Wallets.FindAsync(walletId) ?? throw new Exception("Wallet not found");
            return wallet.Balance;
        }
    }
}