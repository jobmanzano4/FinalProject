using ServiceReference1;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace Soap.Test
{
    [TestClass]
    public class SoapTesting
    {
        CountryInfoServiceSoapTypeClient service = null;
        /// <summary>
        /// Create a private method(A) that calls `ListOfCountryNamesByCode` endpoint with return type list of
        /// tCountryCodeAndName` model
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        private List<tCountryCodeAndName> ListOfCountry(CountryInfoServiceSoapTypeClient service)
        {
            var listOfCountry = service.ListOfCountryNamesByCode();
            return listOfCountry;
        }

        /// <summary>
        /// B.Create a private method(B) that will get a random record from a list.This method accept list
        ///`tCountryCodeAndName` and return type is string or object
        /// </summary>
        /// <param name="listOfCountry"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        private tCountryCodeAndName RandomListofCountryDetails(List<tCountryCodeAndName> listOfCountry)
        {
            Random random = new Random();
            int index = random.Next(0, listOfCountry.Count - 1);
            return listOfCountry[index];
        }

        [TestInitialize]
        public void TestMethod1()
        {
            service = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        }

        [TestMethod]
        public void VerifyFullCountry()
        {
            //Use method(A) from pre-requisite to get the List of Country Name by Code
            var listOfCountryData = ListOfCountry(service);
            //Get a random record using method (B) from pre-requisite
            var randomCountry = RandomListofCountryDetails(listOfCountryData);
            //Use the country code of random record from step(2), as a parameter in `FullCountryInfo`
            var fullCountryDetails = service.FullCountryInfo(randomCountry.sISOCode);
            //Verify response of `FullCountryInfo` endpoint matches the Code and Name with the record from
            //step(3)
            Assert.AreEqual(randomCountry.sISOCode, fullCountryDetails.sISOCode);
            Assert.AreEqual(randomCountry.sName, fullCountryDetails.sName);
        }


        /// <summary>
        ///  Create Test Method with the following steps(10 pts)
        /// </summary>
        [TestMethod]
        public void VerifyCodeMatches()
        {
            //1.Use method(B) from pre-requisite and get 5 country records
            List<tCountryCodeAndName> listOfCountry = new List<tCountryCodeAndName>();
            var FiveCountries = listOfCountry.Take(5);
            //Call `CountryISOCode` endpoint for each record from step (1)
            foreach (var country in FiveCountries)
            {
                var countryISOCode = service.CountryISOCode(country.sName);
                //Verify Country Code matches
                Assert.AreEqual(country.sISOCode, countryISOCode);
            }
        }
    }
}