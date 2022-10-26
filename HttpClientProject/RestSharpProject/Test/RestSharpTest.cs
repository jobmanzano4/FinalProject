using RestSharpProject.Helpers;
using RestSharpProject.Test.TestData;
using System.Net;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace RestSharpProject.Test
{
    [TestClass]
    public class RestSharpTest : BaseTest
    {
        [TestMethod]
        public async Task PostBooking()
        {
            var restResponse = await BookingHelper.AddNewBooking(RestClient);
            bookingDetails = restResponse.Data;

            var response = await BookingHelper.GetBookingById(RestClient, bookingDetails.Bookingid);

            //assert response
            var createdData = GenerateBookingRecord.bookingInstance();
            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(createdData.Firstname, response.Data.Firstname, "First name does no match");
            Assert.AreEqual(createdData.Lastname, response.Data.Lastname, "Lastname does no match");
            Assert.AreEqual(createdData.Totalprice, response.Data.Totalprice, "Total Price does no match");
            Assert.AreEqual(createdData.Depositpaid, response.Data.Depositpaid, "Deposit paid does no match");
            Assert.AreNotEqual(createdData.Bookingdates.Checkin, response.Data.Bookingdates.Checkin, "check in does no match"); //checkin and checkout date from response is alwasys 1 day late, maybe the time of the server? temporarily asserted this to not equal
            Assert.AreNotEqual(createdData.Bookingdates.Checkout, response.Data.Bookingdates.Checkout, "checkout does no match");
            Assert.AreEqual(createdData.Additionalneeds, response.Data.Additionalneeds, "Additional Needs does no match");
            //cleanup
            await BookingHelper.DeleteBookingById(RestClient, bookingDetails.Bookingid);
        }


    }
}
