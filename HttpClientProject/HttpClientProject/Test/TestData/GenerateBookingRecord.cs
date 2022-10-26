using HttpClientProject.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpClientProject.Helpers;

namespace HttpClientProject.Test.TestData
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
                Firstname = "Cat",
                Lastname = "Master",
                Totalprice = 1000,
                Depositpaid = true,

                Bookingdates = bookingdates,
                Additionalneeds = "Breakfast"

            };
        }
    }
}
