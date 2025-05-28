using System;
using System.Data.Entity;
using System.Threading.Tasks;
using WalletService.Business.Entities;
using WalletService.Business.Interfaces;
using WalletService.Data.Context;

namespace WalletService.Data.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletDbContext _context;
        public WalletRepository(WalletDbContext context) => _context = context;

        public Task<Wallet> GetByIdAsync(Guid id) => _context.Wallets.FindAsync(id);
        public Task AddAsync(Wallet wallet) { _context.Wallets.Add(wallet); return Task.CompletedTask; }
        public void Update(Wallet wallet) => _context.Entry(wallet).State = EntityState.Modified;
    }
}
