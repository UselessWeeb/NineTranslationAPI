using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Services.Interfaces;

namespace APINineTranslation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IVnPayService _vnPayService;
        private readonly IEmailService _emailService;

        public DonationController(ITransactionService transactionService, IVnPayService vnPayService, IEmailService emailService)
        {
            _transactionService = transactionService;
            _vnPayService = vnPayService;
            _emailService = emailService;
        }

        [HttpGet("donate")]
        public async Task<IActionResult> DonateAsync([FromQuery] string userEmail, [FromQuery] string description, [FromQuery] decimal amount)
        {
            if (string.IsNullOrEmpty(userEmail) || amount <= 0)
                return BadRequest("Invalid user ID or amount.");

            var transaction = new CreateTransactionDto
            {
                Amount = amount,
                UserEmail = userEmail,
                Description = description,
                TransactionDate = DateTime.UtcNow,
            };

            await _transactionService.CreateTransactionAsync(transaction);

            var allTransactions = await _transactionService.GetAllTransactionsAsync();
            var lastTransaction = allTransactions.OrderByDescending(t => t.Id).First();

            string clientIp = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "127.0.0.1";

            var paymentUrl = _vnPayService.CreatePaymentUrl(
                totalPrice: (float)amount,
                orderId: lastTransaction.Id,
                description: $"Donation from user {userEmail}",
                clientIp: clientIp
            );

            return Ok(paymentUrl);
        }

        [HttpGet("payment-return")]
        public async Task<IActionResult> PaymentReturn()
        {
            var queryParams = HttpContext.Request.Query
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

            if (!_vnPayService.VerifyHash(queryParams))
                return BadRequest("Invalid signature.");

            int orderId = int.Parse(queryParams["vnp_TxnRef"]);
            string responseCode = queryParams["vnp_ResponseCode"];

            if (responseCode == "00")
            {
                await _transactionService.UpdateTransactionState(orderId, TransactionStatus.Completed);
                var transaction = await _transactionService.GetTransactionByIdAsync(orderId);
                if (transaction != null)
                {
                    string subject = "Thank you for your donation!";
                    string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "DonationSuccess.html");
                    string body = await System.IO.File.ReadAllTextAsync(templatePath);

                    body = body.Replace("{{FullName}}", transaction.UserEmail)
                               .Replace("{{Amount}}", transaction.Amount.ToString("N0"));

                    await _emailService.SendEmailAsync(transaction.UserEmail, subject, body);
                }
                return Ok("https://your-frontend.com/success");
            }
            else
            {
                await _transactionService.UpdateTransactionState(orderId, TransactionStatus.Failed);
                return Ok("https://your-frontend.com/failed");
            }
        }

        [Authorize(Roles = "Staff, Admin")]
        [HttpGet("GetTransactionGraph/{month}")]
        public async Task<IActionResult> GetTransactionGraph(int month)
        {
            if (month < 1 || month > 999)
                return BadRequest("Invalid month");
            var transactions = await _transactionService.GetDonationsPerMonthAsync(month);
            if (transactions == null || !transactions.Any())
                return NotFound("No transactions found for the specified month period.");
            return Ok(transactions);
        }

        [Authorize(Roles = "Staff, Admin")]
        [HttpGet("GetTransactionHistory")]
        public async Task<IActionResult> GetTransactionHistory()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            if (transactions == null || !transactions.Any())
                return NotFound("No transactions found.");
            return Ok(transactions);
        }

        [Authorize(Roles = "Staff, Admin")]
        [HttpGet("GetTransactionById/{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound($"Transaction with ID {id} not found.");
            return Ok(transaction);
        }
    }
}
