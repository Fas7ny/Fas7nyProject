using System.ComponentModel.DataAnnotations;

namespace Fas7ny.Domain.Entities
{
    public class BookingCustomTrip
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime BookingDate { get; set; }

        public double TotalPrice { get; set; }


        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone] // التحقق من صحة رقم الهاتف
        public string PhoneNumber { get; set; }

        public ICollection<BookingCustomTripDetail> BookingCustomTripDetail { get; set; }
    }
}
