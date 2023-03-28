using System;
using NUnit.Framework;
using Reporting.Tests.API.Actions;
using Reporting.Tests.API.API.Contexts;
using Reporting.Tests.API.API.Models;
using Reporting.Tests.API.API.Steps;
using TechTalk.SpecFlow;

namespace Reporting.Tests.API.Steps
{
    [Binding]
    public class ReportingSteps : BaseSteps
    {
        private readonly UserContext _userContext;
        private IApiResponseModel _lastResponse;
        private ApiResponseModel<ListResponse> _allUsersResponse;
        private ApiResponseModel<CreateUserResponse> _createUserResponse;

        public ReportingSteps(UserContext userContext)
        {
            _userContext = userContext;
        }

        [When(@"I retrieve all users: page-'(.*)'")]
        public void WhenIRetrieveAllUsers(int page)
        {
            RetrieveAllUsers(page);
            VerifyResponseOk(_lastResponse);
        }

        [When(@"I retrieve all users to fail: page-'(.*)'")]
        public void WhenIRetrieveAllUsersToFail(int page)
        {
            RetrieveAllUsers(page);
        }

        [When(@"I create new user: name-'(.*)' job-'(.*)'")]
        public void WhenICreateNewUser(string name, string job)
        {
            CreateUser(name, job);
            VerifyResponseCreated(_lastResponse);
        }

        [When(@"I create new user to fail: name-'(.*)' job-'(.*)'")]
        public void WhenICreateNewUserToFail(string name, string job)
        {
            CreateUser(name, job);
        }

        [Then(@"I see last response error '(.*)'")]
        public void ThenISeeLastResponseError(string message)
        {
            VerifyResponseError(_lastResponse, message);
        }

        [Then(@"I see all users response values: page-'(.*)' perPage-'(.*)' total-'(.*)' totalPages-'(.*)'")]
        public void ThenISeeLastResponseError(int page, int perPage, int total, int totalPages)
        {
            Assert.That(_allUsersResponse.Data.page, Is.EqualTo(page));
            Assert.That(_allUsersResponse.Data.per_page, Is.EqualTo(perPage));
            Assert.That(_allUsersResponse.Data.total, Is.EqualTo(total));
            Assert.That(_allUsersResponse.Data.total_pages, Is.EqualTo(totalPages));
        }

        [Then(@"I see create user response values: name-'(.*)' job-'(.*)' created-'(.*)'")]
        public void ThenISeeLastResponseError(string name, string job, DateTime? created)
        {
            Assert.That(_createUserResponse.Data.name, Is.EqualTo(name));
            Assert.That(_createUserResponse.Data.job, Is.EqualTo(job));
            Assert.That(_createUserResponse.Data.createdAt?.Date, Is.EqualTo(created?.Date));
        }

        private void RetrieveAllUsers(int clientId)
        {
            _allUsersResponse = Call.Reporting.GetAllUsers(clientId);
            _lastResponse = _allUsersResponse;
        }

        private void CreateUser(string name, string job)
        {
            var createUpdateUserModel = new CreateUpdateUserModel()
            {
                job = job,
                name=name,
            };
            _createUserResponse = Call.Reporting.Create(createUpdateUserModel);
            _lastResponse = _createUserResponse;
            
            if (_createUserResponse.IsSuccess)
            {
                _userContext.AddItem(name, int.Parse(_createUserResponse.Data.id));
            }
        }
    }
}
