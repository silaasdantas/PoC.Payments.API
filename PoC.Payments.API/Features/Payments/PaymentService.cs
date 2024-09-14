using Stripe.Checkout;
using Stripe;

namespace PoC.Payments.API.Features.Payment;

public class PaymentService
{
    private readonly IConfiguration _configuration;

    public PaymentService(IConfiguration configuration)
    {
        _configuration = configuration;
        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
    }

    public async Task<Session> CreateCheckoutSession(decimal amount, string currency = "brl")
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card", "boleto" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(amount * 100), // Stripe usa valores em centavos
                        Currency = currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Mensalidade",
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = "http://localhost:5187/success.html",
            CancelUrl = "http://seu-site.com:5187/cancel.html",
        };

        var service = new SessionService();
        return await service.CreateAsync(options);
    }
}
