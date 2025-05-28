using System;
using System.Data.Entity;
using WalletService.Business.Entities;
using WalletService.Business.Interfaces;
using WalletService.Data.Context;

namespace WalletService.Data.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletDbContext _context;
        public WalletRepository(WalletDbContext context) => _context = context;

        public Wallet GetById(Guid id) => _context.Wallets.Find(id);
        public void Add(Wallet wallet) => _context.Wallets.Add(wallet);
        public void Update(Wallet wallet) => _context.Entry(wallet).State = EntityState.Modified;
        public void SaveChanges() => _context.SaveChanges();
    }
}