using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowRestSharpReqresApi.StepDefinitions
{
    [Binding]
    public class CreateUserStepDefinitions
    {
        private const string BASE_URL = "https://reqres.in";
        private RestClient client; // Declare variable for RestClient

        private readonly ScenarioContext _scenarioContext; // Declare variable for ScenarioContext

        public CreateUserStepDefinitions(ScenarioContext scenarioContext) // Constructor for Scenario Context
        {
            _scenarioContext = scenarioContext;
        }

        private RestResponse createUser()
        {
            client = new RestClient(BASE_URL);
            var request = new RestRequest("/api/users", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(_scenarioContext["payload"]);
            RestResponse apiResponse = client.Execute(request);
            return apiResponse;
        }


        [Given(@"user name ""([^""]*)"" with job ""([^""]*)""")]
        public void GivenUserNameWithJob(string inputName, string inputJob)
        {
            var requestPayload = new {
                name = inputName,
                job = inputJob
            };
            _scenarioContext["payload"] = requestPayload;

        }

        [When(@"request to create user is sent")]
        public void WhenRequestToCreateUserIsSent()
        {
            var jsonPayload = _scenarioContext["payload"];
            Console.WriteLine(jsonPayload);
            var response = createUser();

            _scenarioContext["responseStatusCode"] = (int)response.StatusCode;
            dynamic content = JsonConvert.DeserializeObject(response.Content);
            _scenarioContext["jsonResponse"] = content;
        }

        [Then(@"user is successfully created")]
        public void ThenUserIsSuccessfullyCreated()
        {
            dynamic jsonPayload = _scenarioContext["payload"];
            dynamic jsonResponse = _scenarioContext["jsonResponse"];
            Assert.AreEqual(201, _scenarioContext["responseStatusCode"]);
            Assert.AreEqual(jsonPayload.name, jsonResponse.name.ToObject<string>());
            Assert.AreEqual(jsonPayload.job, jsonResponse.job.ToObject<string>());
        }
    }
}
