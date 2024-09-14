using MediatR;

namespace PoC.Payments.API.Features.Payments.CreateCheckout;

public class CreateCheckoutCommand : IRequest<string>
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "brl";
}
