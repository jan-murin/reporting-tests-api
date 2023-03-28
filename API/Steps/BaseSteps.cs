using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using Reporting.Tests.API.API.Infrastructure;
using Reporting.Tests.API.API.Models;
using RestSharp;

namespace Reporting.Tests.API.API.Steps
{
    public class BaseSteps
    {
        protected void VerifyResponseOk(IApiResponseModel response)
        {
            Assert.That(
                response.Status,
                Is.EqualTo(HttpStatusCode.OK).Or.EqualTo(HttpStatusCode.NoContent),
                $@"Response was not OK. Status code: {response.Status}, message: {response.Error}");
        }
        protected void VerifyResponseCreated(IApiResponseModel response)
        {
            Assert.That(
                response.Status,
                Is.EqualTo(HttpStatusCode.Created).Or.EqualTo(HttpStatusCode.NoContent),
                $@"Response was not OK. Status code: {response.Status}, message: {response.Error}");
        }

        protected void VerifyRestResponseOk(IRestResponse response)
        {
            Assert.That(
                response.StatusCode,
                Is.EqualTo(HttpStatusCode.OK).Or.EqualTo(HttpStatusCode.NoContent),
                $@"Response was not OK. Status code: {response.StatusCode}, message: {response.ErrorMessage}");
        }

        protected static void VerifyResponseError(IApiResponseModel response, string errorMsg)
        {
            Assert.That(
                response.Status,
                Is.Not.EqualTo(HttpStatusCode.OK)
                    .Or.EqualTo(HttpStatusCode.NoContent),
                "Status code: " + response.Status + ", expected: BadRequest; Expected error: " + errorMsg);

            if (EmptyToNull(errorMsg) != null)
            {
                //Currently only works with contains, but once content shows only error message it should be uncommented
                Assert.That(
                    response.Error,
                    Does.Contain(errorMsg),
                    "Error messages do not match, expected: " + errorMsg + ", actual: " + (response.Error ?? "null"));
            } else if (errorMsg.Trim() == "")
            {
                throw new Exception("Please provide expected error message!");
            }
        }

        protected static void VerifyRestResponseError(IRestResponse response, string errorMsg)
        {
            Assert.That(
                response.StatusCode,
                Is.Not.EqualTo(HttpStatusCode.OK)
                    .Or.EqualTo(HttpStatusCode.NoContent),
                "Status code: " + response.StatusCode + ", expected: BadRequest; Expected error: " + errorMsg);

            if (EmptyToNull(errorMsg) != null)
            {
                //Currently only works with contains, but once content shows only error message it should be uncommented
                Assert.That(
                    response.ErrorMessage,
                    Does.Contain(errorMsg),
                    "Error messages do not match, expected: " + errorMsg + ", actual: " + response.ErrorMessage);
            }
            else if (errorMsg.Trim() == "")
            {
                throw new Exception("Please provide expected error message!");
            }
        }

        protected void VerifyResponseNoContent(IApiResponseModel response)
        {
            Assert.That(response.Status, Is.EqualTo(HttpStatusCode.NoContent),
                "Status code: " + response.Status + ", expected: NoContent, message: " + response.Error);
        }

        public static string EmptyToNull(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public void BeforeScenario()
        {
            //Create new customer for every test and set it as default
            ApplicationInfo.RecreateApplicationId();
            Customers.CurrentCustomer = new CustomerInfo();
            Customers.DefaultCustomer = Customers.CurrentCustomer;
        }

        protected IEnumerable<int> GetListOfInts(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return new List<int>();
            }

            return ids.Split(',').Select(x => int.Parse(x)).ToList();
        }

        protected string GetStringWithLength(string input)
        {
            var length = int.Parse(input.Replace("#{", "").Replace("}", ""));
            
            return new string('*', length);
        }
    }
}