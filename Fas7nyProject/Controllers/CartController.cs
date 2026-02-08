using Fas7ny.Application.DTOs.Cart.Request;
using Fas7ny.Application.DTOs.Cart.Response;
using Fas7ny.Domain.Entities;
using Fas7ny.Domain.RepoInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fas7nyProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CartController> _logger;

        public CartController(
            IUnitOfWork unitOfWork,
            ILogger<CartController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("my-cart")]
        public async Task<IActionResult> GetMyCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var cart = await _unitOfWork.Carts.FindAsync(c => c.UserId == userId);

            if (cart == null)
                return Ok(new List<CartItemDetailsResponse>());

            var items = await _unitOfWork.CartItem.FindManyAsync(ci => ci.CartId == cart.Id);

            var response = items.Select(ci => new CartItemDetailsResponse
            {
                Id = ci.Id,
                CartId = ci.CartId,
                BookingId = ci.BookingId,
                Quantity = ci.Quantity,
                Price = ci.Price,
                ItemTotal = ci.Price * ci.Quantity
            });

            return Ok(response);
        }

        [Authorize]
        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem([FromBody] AddCartItemRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var booking = await _unitOfWork.Bookings.GetByIdAsync(dto.BookingId);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            if (booking.UserId != userId)
                return Forbid();

            if (booking.Status != "0" && booking.Status != "Pending")
                return BadRequest(new { message = "Only pending bookings can be added to cart" });

            var cart = await _unitOfWork.Carts.FindAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Carts
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow

                };
                await _unitOfWork.Carts.AddAsync(cart);
                await _unitOfWork.SaveChangesAsync();
            }

            var existingItem = await _unitOfWork.CartItem
                .FindAsync(ci => ci.CartId == cart.Id && ci.BookingId == booking.Id);

            CartItems cartItem;

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                cartItem = existingItem;
                await _unitOfWork.CartItem.UpdateAsync(existingItem);
            }
            else
            {
                int productId = 0;

                cartItem = new CartItems
                {
                    CartId = cart.Id,
                    BookingId = booking.Id,

                    Quantity = dto.Quantity,
                    Price = booking.TotalPrice
                };
                await _unitOfWork.CartItem.AddAsync(cartItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Carts.UpdateAsync(cart);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new
            {
                message = "Item added to cart successfully",
                itemId = cartItem.Id
            });
        }

        [Authorize]
        [HttpPut("items/{itemId:int}")]
        public async Task<IActionResult> UpdateQuantity(
         int itemId,
        [FromBody] UpdateCartItemQuantityRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var cartItem = await _unitOfWork.CartItem.GetByIdAsync(itemId);
            if (cartItem == null)
                return NotFound(new { message = "Cart item not found" });

            var cart = await _unitOfWork.Carts.GetByIdAsync(cartItem.CartId);
            if (cart == null)
                return NotFound(new { message = "Cart not found" });

            if (cart.UserId != userId)
                return Forbid();

            cartItem.Quantity = dto.Quantity;

            await _unitOfWork.CartItem.UpdateAsync(cartItem);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new
            {
                message = "Quantity updated",
                itemTotal = cartItem.Quantity * cartItem.Price
            });
        }


        [HttpDelete("items/{itemId:int}")]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var cartItem = await _unitOfWork.CartItem.GetByIdAsync(itemId);
            if (cartItem == null)
                return NotFound(new { message = "Cart item not found" });

            var cart = await _unitOfWork.Carts.GetByIdAsync(cartItem.CartId);
            if (cart == null || cart.UserId != userId)
                return Forbid();

            await _unitOfWork.CartItem.DeleteAsync(cartItem);

            cart.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { message = "Item removed from cart" });
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var cart = await _unitOfWork.Carts.FindAsync(c => c.UserId == userId);
            if (cart == null)
                return Ok(new { message = "Cart already empty" });

            var items = await _unitOfWork.CartItem.FindManyAsync(ci => ci.CartId == cart.Id);

            if (!items.Any())
                return Ok(new { message = "Cart already empty" });

            await _unitOfWork.CartItem.DeleteRangeAsync(items);

            cart.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new
            {
                message = "Cart cleared successfully",
                itemsRemoved = items.Count()
            });
        }
    }
}