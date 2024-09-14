using TechTalk.SpecFlow;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoC.Payments.FunctionalTests.Steps
{
    [Binding]
    public class CheckoutSteps
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private string _responseContent;

        public CheckoutSteps(WebApplicationFactory<PoC.Payments.API.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Given(@"I have a valid amount to pay")]
        public void GivenIHaveAValidAmountToPay()
        {
            // Prepare any necessary data or context
        }

        [When(@"I request a checkout session")]
        public async Task WhenIRequestACheckoutSession()
        {
            var requestBody = new
            {
                amount = 100.00M, // Valor do pagamento
                currency = "brl"  // Moeda
            };

            _response = await _client.PostAsJsonAsync("/api/payments/create-checkout-session", requestBody);
            _responseContent = await _response.Content.ReadAsStringAsync();
        }

        [Then(@"I should receive a session ID")]
        public void ThenIShouldReceiveASessionID()
        {
            _response.EnsureSuccessStatusCode();
            _responseContent.Should().Contain("sessionId");
        }
    }
}
