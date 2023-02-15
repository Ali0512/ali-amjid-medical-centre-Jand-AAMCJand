using Braintree;

namespace AAMCJand.Data.Services
{
    public interface IBrainTreeGate
    {
       IBraintreeGateway CreateGateway();
        IBraintreeGateway GetGateway();
    }
}
