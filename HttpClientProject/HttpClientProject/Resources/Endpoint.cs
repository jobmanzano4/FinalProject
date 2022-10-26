using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientProject.Resources
{
    public  class Endpoint
    {
        //Base URL
        public const string baseURL = "https://restful-booker.herokuapp.com";
        public static string GetBookingById(long bookingId) => $"{baseURL}/booking/{bookingId}";
        public static string PostBooking() => $"{baseURL}/booking";
        public static string DeleteBooking(long bookingId) => $"{baseURL}/booking/{bookingId}";

        public static string UpdateBookingById(long bookingId) => $"{baseURL}/booking/{bookingId}";
        public static string Authenticate() => $"{baseURL}/auth";

    }
}
