using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

namespace SpecFlowRestSharpReqresApi.StepDefinitions
{
    [Binding]
    public sealed class GetUsersStepDefinitions
    {
        private const string BASE_URL = "https://reqres.in";
        private RestClient client;

        private readonly ScenarioContext _scenarioContext;

        public GetUsersStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        private RestResponse getUserByIdAPi(string id) {
            client = new RestClient(BASE_URL);
            var request = new RestRequest($"/api/users/{_scenarioContext["userId"]}", Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            RestResponse apiResponse = client.Execute(request);
            return apiResponse;
        }

        [Given(@"enter user id ""([^""]*)""")]
        public void GivenEnterUserId(string id)
        {
            _scenarioContext["userId"] = id;

        }

        [When(@"request to get user by user id is sent")]
        public void WhenRequestToGetUserByUserIdIsSent()
        {

            var response = getUserByIdAPi(_scenarioContext["userId"].ToString());

            _scenarioContext["responseStatusCode"] = (int)response.StatusCode;
            dynamic content = JsonConvert.DeserializeObject(response.Content);
            _scenarioContext["jsonResponse"] = content;
        }

        [Then(@"details of user id ""([^""]*)"" is displayed")]
        public void ThenDetailsOfUserIdIsDisplayed(string userId)
        {
            dynamic jsonResponse = _scenarioContext["jsonResponse"];

            Assert.AreEqual(200, _scenarioContext["responseStatusCode"]);
            //Assert.AreEqual(int.Parse(userId), jsonResponse.data.id.ToObject<int>());
            Assert.AreEqual(userId, jsonResponse.data.id.ToObject<string>());
        }

    }
}