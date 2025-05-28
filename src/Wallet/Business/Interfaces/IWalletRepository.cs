using System;
using System.Threading.Tasks;
using WalletService.Business.Entities;

namespace WalletService.Business.Interfaces
{
    public interface IWalletRepository
    {
        Task<Wallet> GetByIdAsync(Guid id);
        Task AddAsync(Wallet wallet);
        void Update(Wallet wallet);
    }
}
