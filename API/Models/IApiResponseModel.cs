using System.Net;

namespace Reporting.Tests.API.API.Models
{
    public interface IApiResponseModel
    {
        HttpStatusCode Status { get; }
        bool IsSuccess { get; }
        bool IsFailure { get; }
        string Error { get; }
        string Content { get; }
    }
}
