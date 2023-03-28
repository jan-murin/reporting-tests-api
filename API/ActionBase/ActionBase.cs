using System.Collections.Generic;
using System.Net;
using Reporting.Tests.API.API.Models;
using RestSharp;

namespace Reporting.Tests.API.API.ActionBase
{
    public abstract class ActionBase
    {
        private readonly string _resource;

        protected ActionBase(string resource)
        {
            _resource = resource;

            // makes SSL/TLS work
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        protected RestRequest CreateRequest(Method httpMethod, string methodName)
        {
            var request = new RestRequest
            {
                Resource = $"{_resource}/{methodName}",
                RequestFormat = DataFormat.Json,
                Method = httpMethod
            };
            return request;
        }

        protected abstract ApiResponseModel<T> Execute<T>(RestRequest request);

        protected void LogToCallReport(RestRequest request)
        {
            // TODO: enable call report
            //CallReport.CallReportGenerator.Log(TestContext.CurrentContext.Test, request, false);
        }

        // TODO: can we get rid of this by using some option on api calls for ignoring null values?
        protected IDictionary<string, object> GetNonNullPropertyValues(object obj)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (var property in obj.GetType().GetProperties())
            {
                var propertyValue = property.GetValue(obj, null);
                if (propertyValue != null)
                {
                    dictionary.Add(property.Name, propertyValue);
                }
            }
            return dictionary;
        }
    }
}
