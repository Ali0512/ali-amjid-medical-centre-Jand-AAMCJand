using AAMCJand.Data.BrainTree;
using Braintree;
using Microsoft.Extensions.Options;

namespace AAMCJand.Data.Services
{
    public class BrainTreeGateService : IBrainTreeGate
    {
        BrainTreeSettings _options { get; set; }
        IBraintreeGateway braintreeGateway { get; set; }
        public BrainTreeGateService(IOptions<BrainTreeSettings> options)
        {
            _options = options.Value;
        }
        public IBraintreeGateway CreateGateway()
        {
            return new BraintreeGateway(_options.Environment, _options.MerchantId, _options.PublicKey, _options.PrivateKey);
        }

        public IBraintreeGateway GetGateway()
        {
            if(braintreeGateway == null)
            {
                braintreeGateway = CreateGateway();
            }
            return braintreeGateway;
        }
    }
}
