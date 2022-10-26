using RestSharp;
using RestSharpProject.DataModels;

namespace RestSharpProject.Test
{
    public  class BaseTest
    {
        public RestClient RestClient { get; set; }

        public BookingModelRoot bookingDetails { get; set; }

        [TestInitialize]
        public void Initilize()
        {
            RestClient = new RestClient();
        }

        [TestCleanup]
        public void CleanUp()
        {

        }

    }
}
