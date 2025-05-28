using System;
using System.Threading.Tasks;

namespace WalletService.Business.Interfaces
{
    public interface ITransactionService
    {
        Task<Guid> CreateWalletAsync(Guid customerId, string currency);
        Task DepositAsync(Guid walletId, Guid transactionId, decimal amount); //WIN
        Task WithdrawAsync(Guid walletId, Guid transactionId, decimal amount); //BET
        Task<decimal> GetBalanceAsync(Guid walletId);
    }

}
