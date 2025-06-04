using PayPalCheckoutSdk.Core;
using PayPalHttpClient = PayPalHttp.HttpClient;
using SystemHttpClient = System.Net.Http.HttpClient;
namespace WebShopNovi.Services
{
    public class PayPalClient
    {
        private readonly PayPalEnvironment _environment;
        private readonly PayPalHttpClient _client;

        public PayPalClient(string clientId, string secret, bool isSandbox = true)
        {
            if (isSandbox)
                _environment = new SandboxEnvironment(clientId, secret);
            else
                _environment = new LiveEnvironment(clientId, secret);

            _client = new PayPalHttpClient(_environment);
        }

        public PayPalHttpClient Client => _client;
    }
}
