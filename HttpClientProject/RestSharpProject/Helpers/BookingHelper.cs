using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using RestSharpProject.DataModels;
using Newtonsoft.Json;
using RestSharpProject.Resources;
using RestSharpProject.Test.TestData;
using RestSharp;

namespace RestSharpProject.Helpers
{
   
    public class BookingHelper
    {
        //RestResponse - Container for data sent back from API including deserialized data
        public static async Task<RestResponse<BookingModelRoot>> AddNewBooking(RestClient restClient)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            var postRequest = new RestRequest(Endpoint.PostBooking()).AddJsonBody(GenerateBookingRecord.bookingInstance());

            return await restClient.ExecutePostAsync<BookingModelRoot>(postRequest);

        }

        public static async Task<RestResponse<BookingModel>> GetBookingById(RestClient restClient, int bookingId)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            var getRequest = new RestRequest(Endpoint.GetBookingById(bookingId));

            return await restClient.ExecuteGetAsync<BookingModel>(getRequest);
        }

        public static async Task<RestResponse> DeleteBookingById(RestClient restClient, int bookingId)
        {
            var token = await GetAuthenticationToken(restClient);
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", "token=" + token);

            var getRequest = new RestRequest(Endpoint.DeleteBooking(bookingId));

            return await restClient.DeleteAsync(getRequest);
        }

        public static async Task<RestResponse<BookingModel>> UpdateBookingById(RestClient restClient, BookingModel booking, int bookingId)
        {
            var token = await GetAuthenticationToken(restClient);
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", "token=" + token);

            var putRequest = new RestRequest(Endpoint.UpdateBookingById(bookingId)).AddJsonBody(booking);

            return await restClient.ExecutePutAsync<BookingModel>(putRequest);
        }

        private static async Task<string> GetAuthenticationToken(RestClient restClient)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            var postRequest = new RestRequest(Endpoint.Authenticate()).AddJsonBody(Authentication.userModel());

            var generateToken = await restClient.ExecutePostAsync<TokenModel>(postRequest);

            return generateToken.Data.Token;
        }

    }
}
