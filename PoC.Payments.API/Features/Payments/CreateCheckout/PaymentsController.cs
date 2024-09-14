using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace PoC.Payments.API.Features.Payments.CreateCheckout;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutCommand command)
    {
        var sessionId = await _mediator.Send(command);
        return Ok(new { sessionId });
    }

    [HttpPost("stripe-webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                "sua-chave-assinatura"
            );

            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                // Atualizar status do pagamento no banco de dados
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest();
        }
    }

   
}
