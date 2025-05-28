using System;
using System.Threading.Tasks;
using WalletService.Business.Entities;

namespace WalletService.Api.Business.Interfaces
{
    public interface IWalletTransactionRepository
    {
        Task<bool> ExistsAsync(Guid transactionId);
        Task AddAsync(WalletTransaction transaction);
    }
}
