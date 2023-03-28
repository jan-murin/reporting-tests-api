using Reporting.Tests.API.API.Extensions;
using Reporting.Tests.API.API.Models;
using RestSharp;

namespace Reporting.Tests.API.API.ActionBase
{
    public class MicroserviceActionBase : ActionBase
    {

        static MicroserviceActionBase()
        {
            //LoadConfiguration();
        }

        public static string AppBaseUrl;
        public static string CallingApplicationName;

        public MicroserviceActionBase(string resource) : base(resource)
        {
            LoadConfiguration();
        }

        protected override ApiResponseModel<T> Execute<T>(RestRequest request)
        {
            var client = new RestClient(AppBaseUrl);

            var response = client.ExecuteForApi<T>(request);

            return response;
        }

        protected ApiResponseModel<T> ExecuteUnauthorized<T>(RestRequest request)
        {
            var client = new RestClient(AppBaseUrl);
            var response = client.ExecuteForApi<T>(request);

            return response;
        }

        protected IRestResponse Execute(RestRequest request)
        {
            var client = new RestClient($"{AppBaseUrl}");
            var response = client.Execute(request);
            return response;
        }

        private static void LoadConfiguration()
        {
            AppBaseUrl = "https://reqres.in/api";
        }
    }
}
