using System;

namespace WalletService.Api.DTOs
{
    public class WalletTransactionRequest
    {
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }
    }
}