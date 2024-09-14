using MediatR;
using PoC.Payments.API.Features.Payment;

namespace PoC.Payments.API.Features.Payments.CreateCheckout;

public class CreateCheckoutHandler : IRequestHandler<CreateCheckoutCommand, string>
{
    private readonly PaymentService _paymentService;

    public CreateCheckoutHandler(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task<string> Handle(CreateCheckoutCommand request, CancellationToken cancellationToken)
    {
        var session = await _paymentService.CreateCheckoutSession(request.Amount, request.Currency);
        return session.Id; // Retorna o sessionId
    }
}
