using Reporting.Tests.API.API.ActionBase;
using Reporting.Tests.API.API.Models;
using RestSharp;

namespace Reporting.Tests.API.Actions
{
    public class ReportingActions : MicroserviceActionBase
    {
        public ReportingActions() : base("/users")
        {
        }

        public ApiResponseModel<ListResponse> GetUser(int id)
        {
            var request = CreateRequest(Method.GET, $"/{id}");
            var result = Execute<ListResponse>(request);
            return result;
        }

        public ApiResponseModel<ListResponse> GetAllUsers(int page)
        {
            var request = CreateRequest(Method.GET, "");
            request.AddParameter("page", page);
            var result = Execute<ListResponse>(request);
            return result;
        }
        
        public ApiResponseModel<CreateUserResponse> Create(CreateUpdateUserModel model)
        {
            var request = CreateRequest(Method.POST, "");
            request.AddJsonBody(model);
            var result = Execute<CreateUserResponse>(request);
            return result;
        }
    }
}
