using Microsoft.Extensions.Configuration;
using Moq;
using PoC.Payments.API.Features.Payment;
using Stripe.Checkout;


namespace PoC.Payments.UnitTests;

public class PaymentServiceTests
{
    [Fact]
    public async Task CreateCheckoutSession_ShouldReturnValidSession()
    {
        // Arrange
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["Stripe:SecretKey"]).Returns("sk_test_12345");

        var paymentService = new PaymentService(configMock.Object);

        decimal amount = 100.00M; // Exemplo de valor
        string currency = "brl";

        // Act
        var result = await paymentService.CreateCheckoutSession(amount, currency);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Session>(result);
        Assert.Equal("payment", result.Mode);
    }
}