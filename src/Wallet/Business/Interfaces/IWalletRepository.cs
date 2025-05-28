using System;
using WalletService.Business.Entities;

namespace WalletService.Business.Interfaces
{
    public interface IWalletRepository
    {
        Wallet GetById(Guid id);
        void Add(Wallet wallet);
        void Update(Wallet wallet);
        void SaveChanges();
    }
}
