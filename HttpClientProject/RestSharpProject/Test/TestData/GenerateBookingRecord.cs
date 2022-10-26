using RestSharp;
using RestSharpProject.DataModels;
using RestSharpProject.Helpers;

namespace RestSharpProject.Test.TestData
{
    public class GenerateBookingRecord
    {
        public static BookingModel bookingInstance() 
        {
            TestDataHelper testDataHelper = new TestDataHelper();
            Bookingdates bookingdates = new Bookingdates();
            bookingdates.Checkin = testDataHelper.dateGenerator();
            bookingdates.Checkout = testDataHelper.dateGenerator().AddDays(5);
            return new BookingModel
            {
                Firstname = "Catu",
                Lastname = "Masteru",
                Totalprice = 10,
                Depositpaid = true,

                Bookingdates = bookingdates,
                Additionalneeds = "Lunch"

            };
        }
    }
}
