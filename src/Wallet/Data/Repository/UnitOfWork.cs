using System.Threading.Tasks;
using WalletService.Api.Business.Interfaces;
using WalletService.Business.Interfaces;
using WalletService.Data.Context;
using WalletService.Data.Repository;

namespace WalletService.Api.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WalletDbContext _context;

        public UnitOfWork(WalletDbContext context)
        {
            _context = context;
            Wallets = new WalletRepository(_context);
            Transactions = new WalletTransactionRepository(_context);
        }

        public IWalletRepository Wallets { get; }
        public IWalletTransactionRepository Transactions { get; }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}