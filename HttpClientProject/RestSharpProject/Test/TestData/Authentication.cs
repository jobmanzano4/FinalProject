using RestSharpProject.DataModels;
namespace RestSharpProject.Test.TestData
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
