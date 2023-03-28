using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Reporting.Tests.API.API.Models;
using RestSharp;

namespace Reporting.Tests.API.API.Extensions
{
    public static class RestSharpExtension
    {
        public static ApiResponseModel<T> ExecuteForApi<T>(this RestClient client, RestRequest request)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            var res = client.Execute(request);
            stopWatch.Stop();

            var result = new ApiResponseModel<T>(res.StatusCode, res.Content);


            if (res.StatusCode != HttpStatusCode.OK)
            {
                LogRequestAndResponse(client, request, res, stopWatch.ElapsedMilliseconds);
            }

            return result;
        }

        private static void LogRequestAndResponse(RestClient client, IRestRequest request, IRestResponse response, long durationMs)
        {
            var requestToLog = new
            {
                resource = request.Resource,
                uri = client.BuildUri(request),
                method = request.Method.ToString(),
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                })
            };

            var responseToLog = new
            {
                responseUri = response.ResponseUri,
                statusCode = response.StatusCode,
                errorMessage = response.ErrorMessage,
                headers = response.Headers,
                content = response.Content
            };

            Console.WriteLine($@"Request completed in {durationMs} ms.");
            Console.WriteLine($@"Request: {JsonConvert.SerializeObject(requestToLog)}");
            Console.WriteLine($@"Response: {JsonConvert.SerializeObject(responseToLog)}");
        }
    }
}
