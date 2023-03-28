using System.Net;
using Utf8Json;

namespace Reporting.Tests.API.API.Models
{
    public class ApiResponseModel<T> : IApiResponseModel
    {
        private readonly string _content;
        public ApiResponseModel(HttpStatusCode status, string content)
        {
            Status = status;
            _content = content;
        }

        public HttpStatusCode Status { get; }
        public bool IsSuccess => Status == HttpStatusCode.OK || Status == HttpStatusCode.NoContent;
        public bool IsFailure => !IsSuccess;
        public string Content { get; }
        public T Data => Status == HttpStatusCode.OK || Status == HttpStatusCode.Created ? JsonSerializer.Deserialize<T>(_content) : default(T);
        public string Error => Status != HttpStatusCode.OK ? _content : null;
    }
}
