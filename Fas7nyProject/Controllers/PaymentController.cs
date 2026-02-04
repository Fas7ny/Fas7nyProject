using Fas7ny.Application.DTOs.Payment.Request;
using Fas7ny.Application.ServivesInterfaces;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymobService _Service;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentController(IConfiguration configuration, IPaymobService paymobService, IUnitOfWork unitOfWork)
        {
            _Service = paymobService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;

        }

        [HttpPost("pay")]
        public async Task<IActionResult> CreatePay(CreatePaymentRequest dto, int id)
        {
            var booking = await _unitOfWork.BookingCustomTrips.GetByIdAsync(dto.BookingId);
            if (booking == null)
                return NotFound(new { message = "Custom trip booking not found" });

            decimal totalPrice = (decimal)booking.TotalPrice;

            var token = await _Service.GetTokenAsync();
            var orderId = await _Service.CreateOrderAsync(token, totalPrice);
            var paymentKey = await _Service.GetPaymentKeyAsync(token, orderId, dto, totalPrice);

            var payment = new Payment
            {
                BookingId = booking.Id,
                Amount = totalPrice,
                Status = "Pending",
                PaymentMethod = "CustomTrip",
                PaymentDate = DateTime.Now,
            };

            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();
            var BaseUrl = _configuration.GetSection("BaseUrl");

            string paymentUrl = $"https://accept.paymob.com/api/acceptance/iframes/{BaseUrl}?payment_token={paymentKey}";

            return Ok(new { PaymentUrl = paymentUrl, TotalPrice = totalPrice });



        }

        [HttpPost("payment-webhook")]
        public async Task<IActionResult> PaymentWebhook([FromBody] PaymobWebhookData data)
        {
            var payment = await _unitOfWork.Payments.FindAsync(p => p.CustomTripBookingId == data.MerchantOrderId);
            if (payment == null)
                return NotFound(new { message = "Payment not found" });

            if (data.Success)
            {
                payment.Status = "Paid";
                var booking = await _unitOfWork.BookingCustomTrips.GetByIdAsync(payment.CustomTripBookingId);
                if (booking != null)
                {
                    return Ok(new { booking });
                }
            }
            else
            {
                payment.Status = "Failed";
            }

            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
