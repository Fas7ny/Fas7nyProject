using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fas7ny.Infrastructure.Hangfire
{
    public interface IBookingJob
    {
        Task ConfirmBooking(int bookingId);

    }
}
