using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace PoC.Payments.IntegrationTests
{
    public class PaymentIntegrationTests : IClassFixture<WebApplicationFactory<PoC.Payments.API.Program>>
    {
        private readonly WebApplicationFactory<PoC.Payments.API.Program> _factory;
        private readonly HttpClient _client;

        public PaymentIntegrationTests(WebApplicationFactory<PoC.Payments.API.Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task CreateCheckoutSession_ShouldReturnSuccess()
        {
            // Arrange
            var requestBody = new
            {
                amount = 100.00M,  // Valor do pagamento
                currency = "brl"   // Moeda
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/payments/create-checkout-session", requestBody);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("sessionId");  // Verifica se o "sessionId" está no resultado
        }
    }
}