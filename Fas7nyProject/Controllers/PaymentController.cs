using Fas7ny.Application.DTOs.Payment.Request;
using Fas7ny.Application.ServivesInterfaces;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymobService _paymob;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentController(
        IPaymobService paymob,
        IConfiguration configuration,
        IUnitOfWork unitOfWork)
    {
        _paymob = paymob;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }


    [Authorize]
    [HttpGet("status/{bookingId:int}")]
    public async Task<IActionResult> GetPaymentStatus(int bookingId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
        if (booking == null)
            return NotFound(new { message = "Booking not found" });

        if (booking.UserId != userId && !User.IsInRole("Admin"))
            return Forbid();

        var payment = await _unitOfWork.Payments
            .FindAsync(p => p.BookingId == bookingId);

        if (payment == null)
        {
            return Ok(new
            {
                bookingId,
                paymentStatus = "NotPaid",
                amount = booking.TotalPrice
            });
        }

        return Ok(new
        {
            bookingId,
            paymentStatus = payment.Status,
            amount = payment.Amount,
            paymentMethod = payment.PaymentMethod,
            paymentDate = payment.PaymentDate
        });
    }



    [Authorize]
    [HttpPost("pay")]
    public async Task<IActionResult> CreatePay([FromBody] CreatePaymentRequest dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var booking = await _unitOfWork.Bookings.GetByIdAsync(dto.BookingId);
        if (booking == null)
            return NotFound(new { message = "Booking not found" });

        if (booking.Status == "Confirmed")
            return BadRequest(new { message = "Booking already paid" });

        var existingPayment = await _unitOfWork.Payments
            .FindAsync(p => p.BookingId == booking.Id && p.Status == "Pending");

        if (existingPayment != null)
        {
            return Ok(new
            {
                success = true,
                paymentUrl = existingPayment.PaymentUrl,
                totalPrice = existingPayment.Amount
            });
        }

        var totalPrice = booking.TotalPrice;

        var token = await _paymob.GetTokenAsync();
        var paymobOrderId = await _paymob.CreateOrderAsync(token, totalPrice);

        var paymentKey = await _paymob.GetPaymentKeyAsync(
    token,
    paymobOrderId,
    dto,
    totalPrice
);


        var iframeId = _configuration["Paymob:IframeId"];
        var paymentUrl =
            $"https://accept.paymob.com/api/acceptance/iframes/{iframeId}?payment_token={paymentKey}";

        var payment = new Payment
        {
            BookingId = booking.Id,
            Amount = totalPrice,
            PaymobOrderId = paymobOrderId,
            Status = "Pending",
            PaymentMethod = "CustomTrip",
            PaymentDate = DateTime.UtcNow,
            PaymentUrl = paymentUrl
        };

      
        await _unitOfWork.Payments.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            success = true,
            paymentUrl,
            totalPrice
        }); 
    }


    [Authorize]
    [HttpPost("cancel/{bookingId:int}")]
    public async Task<IActionResult> CancelPayment(int bookingId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
        if (booking == null)
            return NotFound(new { message = "Booking not found" });

        if (booking.UserId != userId && !User.IsInRole("Admin"))
            return Forbid();

        var payment = await _unitOfWork.Payments
            .FindAsync(p => p.BookingId == bookingId && p.Status == "Pending");

        if (payment == null)
            return BadRequest(new { message = "No pending payment to cancel" });

        payment.Status = "Cancelled";
        booking.Status = "Pending";

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            success = true,
            message = "Payment cancelled successfully"
        });
    }


    [Authorize]
    [HttpGet("my-payments")]
    public async Task<IActionResult> GetMyPayments()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var bookings = await _unitOfWork.Bookings
            .FindAllAsync(b => b.UserId == userId);

        var bookingIds = bookings.Select(b => b.Id).ToList();

        var payments = await _unitOfWork.Payments
            .FindAllAsync(p => bookingIds.Contains(p.BookingId));

        var result = payments
            .OrderByDescending(p => p.PaymentDate)
            .Select(p => new
            {
                paymentId = p.Id,
                bookingId = p.BookingId,
                amount = p.Amount,
                status = p.Status,
                paymentMethod = p.PaymentMethod,
                paymentDate = p.PaymentDate
            });

        return Ok(new
        {
            success = true,
            count = result.Count(),
            payments = result
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("refund/{bookingId:int}")]
    public async Task<IActionResult> RefundPayment(int bookingId)
    {
        var payment = await _unitOfWork.Payments
            .FindAsync(p => p.BookingId == bookingId && p.Status == "Paid");

        if (payment == null)
            return BadRequest(new { message = "No paid payment found for this booking" });

        var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
        if (booking == null)
            return NotFound(new { message = "Booking not found" });

        var token = await _paymob.GetTokenAsync();

        await _paymob.RefundAsync(
            token,
            payment.PaymobOrderId,
            payment.Amount
        );

        payment.Status = "Refunded";
        booking.Status = "Cancelled";

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            success = true,
            message = "Payment refunded successfully",
            refundedAmount = payment.Amount
        });
    }


    [AllowAnonymous]
    [HttpPost("payment-webhook")]
    public async Task<IActionResult> PaymentWebhook([FromBody] PaymobWebhookData data)
    {
        var payment = await _unitOfWork.Payments
            .FindAsync(p => p.PaymobOrderId == data.OrderId);

        if (payment == null)
            return Ok();

        if (payment.Status == "Paid")
            return Ok();

        if (data.Success)
        {
            payment.Status = "Paid";

            var booking = await _unitOfWork.Bookings.GetByIdAsync(payment.BookingId);
            if (booking != null)
                booking.Status = "Confirmed";
        }
        else
        {
            payment.Status = "Failed";
        }

        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }
}
