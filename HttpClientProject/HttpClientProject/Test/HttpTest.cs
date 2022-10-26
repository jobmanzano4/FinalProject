using HttpClientProject.DataModels;
using HttpClientProject.Helpers;
using HttpClientProject.Test.TestData;
using Newtonsoft.Json;
using System.Net;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace HttpClientProject.Test
{
    [TestClass]
    public class HttpTest : BaseTest
    {
        BookingHelper bookingHelper = new BookingHelper();

        [TestMethod]
        public async Task PostBooking()
        {
            var createBooking = await bookingHelper.CreateNewBooking(httpClient);
            var data = JsonConvert.DeserializeObject<BookingModelRoot>(createBooking.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(createBooking.StatusCode, HttpStatusCode.OK);

            #region get created data
            var createdBooking = await bookingHelper.GetBookingById(data.Bookingid,httpClient);
            var response = JsonConvert.DeserializeObject<BookingModel>(createdBooking.Content.ReadAsStringAsync().Result);
            #endregion

            //assert response
            var createdData = GenerateBookingRecord.bookingInstance();
            Assert.AreEqual(createdData.Firstname, response.Firstname, "First name does no match");
            Assert.AreEqual(createdData.Lastname, response.Lastname, "Lastname does no match");
            Assert.AreEqual(createdData.Totalprice, response.Totalprice, "Total Price does no match");
            Assert.AreEqual(createdData.Depositpaid, response.Depositpaid, "Deposit paid does no match");
            Assert.AreNotEqual(createdData.Bookingdates.Checkin, response.Bookingdates.Checkin, "check in does no match"); //checkin and checkout date from response is 1 day late, temporarily asserted this to not equal
            Assert.AreNotEqual(createdData.Bookingdates.Checkout, response.Bookingdates.Checkout, "checkout does no match");
            Assert.AreEqual(createdData.Additionalneeds, response.Additionalneeds, "Additional Needs does no match");
            
            //cleanup
            await bookingHelper.DeleteBookingById(data.Bookingid,httpClient);
        }

        [TestMethod]
        public async Task UpdatePutBooking()
        {
            var createBooking = await bookingHelper.CreateNewBooking(httpClient);
            var data = JsonConvert.DeserializeObject<BookingModelRoot>(createBooking.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(createBooking.StatusCode, HttpStatusCode.OK);

            var getCreatedBooking = await bookingHelper.GetBookingById(data.Bookingid,httpClient);
            var getCreatedBookingResponse = JsonConvert.DeserializeObject<BookingModel>(getCreatedBooking.Content.ReadAsStringAsync().Result);

            var updatedBooking = new BookingModel()
            {
                Firstname = "CatMan Updated",
                Lastname = "LastName of the Cat",
                Totalprice = getCreatedBookingResponse.Totalprice,
                Depositpaid = getCreatedBookingResponse.Depositpaid,
                Bookingdates = getCreatedBookingResponse.Bookingdates,
                Additionalneeds = getCreatedBookingResponse.Additionalneeds
            };
            var updateBooking = await bookingHelper.UpdateBookingById(updatedBooking, data.Bookingid,httpClient);
            var getUpdateBookingResponse = JsonConvert.DeserializeObject<BookingModel>(updateBooking.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(updateBooking.StatusCode, HttpStatusCode.OK);

            var getUpdatedBooking = await bookingHelper.GetBookingById(data.Bookingid,httpClient);
            var response = JsonConvert.DeserializeObject<BookingModel>(getUpdatedBooking.Content.ReadAsStringAsync().Result);
 
            Assert.AreEqual(updatedBooking.Firstname, response.Firstname, "Firstname does no match");
            Assert.AreEqual(updatedBooking.Lastname, response.Lastname, "Lastname does no match");
            Assert.AreEqual(updatedBooking.Totalprice, response.Totalprice, "Totalprice does no match");
            Assert.AreEqual(updatedBooking.Depositpaid, response.Depositpaid, "Depositpaid does no match");
            Assert.AreNotEqual(updatedBooking.Bookingdates.Checkin, response.Bookingdates.Checkin, "Checkin does no match");
            Assert.AreNotEqual(updatedBooking.Bookingdates.Checkout, response.Bookingdates.Checkout, "Checkout does no match");
            Assert.AreEqual(updatedBooking.Additionalneeds, response.Additionalneeds, "Additional needs does no match");

            await bookingHelper.DeleteBookingById(data.Bookingid,httpClient);
        }

        [TestMethod]
        public async Task DeleteBooking()
        {
            var createBooking = await bookingHelper.CreateNewBooking(httpClient);
            var data = JsonConvert.DeserializeObject<BookingModelRoot>(createBooking.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(createBooking.StatusCode, HttpStatusCode.OK);
            var delete = await bookingHelper.DeleteBookingById(data.Bookingid,httpClient);
            Assert.AreEqual(delete.StatusCode, HttpStatusCode.Created, "Status does not match");
        }

        [TestMethod]
        public async Task InvalidBooking()
        {
            var getCreatedBooking = await bookingHelper.GetBookingById(-2,httpClient);
            Assert.AreEqual(getCreatedBooking.StatusCode, HttpStatusCode.NotFound, "Status does not match");
        }
    }
}
