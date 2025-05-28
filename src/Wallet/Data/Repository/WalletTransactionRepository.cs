using System;
using System.Data.Entity;
using System.Threading.Tasks;
using WalletService.Api.Business.Interfaces;
using WalletService.Business.Entities;
using WalletService.Data.Context;

namespace WalletService.Api.Data.Repository
{
    public class WalletTransactionRepository : IWalletTransactionRepository
    {
        private readonly WalletDbContext _context;
        public WalletTransactionRepository(WalletDbContext context) => _context = context;

        public Task<bool> ExistsAsync(Guid transactionId) => _context.WalletTransactions.AnyAsync(t => t.Id == transactionId);
        public Task AddAsync(WalletTransaction transaction) { _context.WalletTransactions.Add(transaction); return Task.CompletedTask; }
    }
}