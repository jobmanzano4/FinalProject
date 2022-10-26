using HttpClientProject.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientProject.Test
{
    public  class BaseTest
    {
        public HttpClient httpClient { get; set; }

        public BookingModel bookingDetails { get; set; }

        [TestInitialize]
        public void Initilize()
        {
            httpClient = new HttpClient();
        }

        [TestCleanup]
        public void CleanUp()
        {

        }

    }
}
