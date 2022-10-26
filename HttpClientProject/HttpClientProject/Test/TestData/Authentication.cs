using HttpClientProject.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientProject.Test.TestData
{
    public  class Authentication
    {
        public static UserModel userModel() {
            return new UserModel
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}
