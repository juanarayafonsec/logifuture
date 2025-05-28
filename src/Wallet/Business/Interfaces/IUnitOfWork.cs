using System.Threading.Tasks;
using WalletService.Business.Interfaces;

namespace WalletService.Api.Business.Interfaces
{
    public interface IUnitOfWork
    {
        IWalletRepository Wallets { get; }
        IWalletTransactionRepository Transactions { get; }
        Task SaveChangesAsync();
    }
}
