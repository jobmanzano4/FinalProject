using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using HttpClientProject.DataModels;
using Newtonsoft.Json;
using HttpClientProject.Resources;
using HttpClientProject.Test.TestData;

namespace HttpClientProject.Helpers
{
   
    public class BookingHelper
    {

        private async Task<string> GetAuthenticationToken(HttpClient client)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var request = JsonConvert.SerializeObject(Authentication.userModel());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(Endpoint.Authenticate(), postRequest);

            var token = JsonConvert.DeserializeObject<TokenModel>(httpResponse.Content.ReadAsStringAsync().Result);

            return token.Token;
        }
        public async Task<HttpResponseMessage> CreateNewBooking(HttpClient client)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var request = JsonConvert.SerializeObject(GenerateBookingRecord.bookingInstance());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");
            var x = await client.PostAsync(Endpoint.PostBooking(), postRequest);
            return x;
        }

        public async Task<HttpResponseMessage> GetBookingById(int id, HttpClient client)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return await client.GetAsync(Endpoint.GetBookingById(id));
        }

        public async Task<HttpResponseMessage> DeleteBookingById(int id,HttpClient client)
        {
            var token = await GetAuthenticationToken(client);
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Cookie", "token=" + token);

            return await client.DeleteAsync(Endpoint.DeleteBooking(id));
        }

        public async Task<HttpResponseMessage> UpdateBookingById(BookingModel bookingModel, int id, HttpClient client)
        {
            var token = await GetAuthenticationToken(client);
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Cookie", "token=" + token);

            var request = JsonConvert.SerializeObject(bookingModel);
            var putRequest = new StringContent(request, Encoding.UTF8, "application/json");

            return await client.PutAsync(Endpoint.UpdateBookingById(id), putRequest);
        }
        
    }
}
