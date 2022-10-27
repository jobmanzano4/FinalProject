using ServiceReference1;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;

namespace Soap.Test
{
    [TestClass]
    public class SoapTesting
    {
        /*Create a private method(A) that calls `ListOfCountryNamesByCode` endpoint with return type list of
        `tCountryCodeAndName` model
        B.Create a private method(B) that will get a random record from a list.This method accept list
        `tCountryCodeAndName` and return type is string
        */
        CountryInfoServiceSoapTypeClient service = null;
        
    }
}