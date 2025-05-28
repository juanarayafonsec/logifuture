using System;
using System.Threading.Tasks;
using System.Web.Http;
using WalletService.Api.DTOs;
using WalletService.Business.Interfaces;

namespace WalletService.Api.Controllers
{
    [RoutePrefix("api/wallets")]
    public class WalletsController : ApiController
    {
        private readonly ITransactionService _transactionService;
        public WalletsController(ITransactionService walletService) => _transactionService = walletService;

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> CreateWallet([FromBody] CreateWalletRequest request)
        {
            try
            {
                var id = await _transactionService.CreateWalletAsync(request.CustomerId, request.Currency);
                return Ok(new { WalletId = id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch, Route("{id}/balance")]
        public async Task<IHttpActionResult> Withdraw(Guid id, [FromBody] WalletTransactionRequest request)
        {
            await _transactionService.WithdrawAsync(id, request.TransactionId, request.Amount);
            return Ok();
        }

        [HttpPost, Route("{id}/add")]
        public async Task<IHttpActionResult> Deposit(Guid id, [FromBody] WalletTransactionRequest request)
        {
            await _transactionService.DepositAsync(id, request.TransactionId, request.Amount);
            return Ok();
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> GetBalance(Guid id)
        {
            var balance = await _transactionService.GetBalanceAsync(id);
            return Ok(new { Balance = balance });
        }
    }

}