using System;

namespace WalletService.Api.DTOs
{
    public class CreateWalletRequest
    {
        public Guid CustomerId { get; set; }
        public string Currency { get; set; }
    }
}