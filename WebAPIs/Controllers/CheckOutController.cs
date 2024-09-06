using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Nodes;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly string _clientId;
        private readonly string _secret;
        private readonly string _url;
        private readonly IHttpClientFactory _httpClientFactory;

        public CheckOutController (IConfiguration config , IHttpClientFactory httpClientFactory)
        {
            _clientId = config ["PayPalSettings:ClientId"] ?? throw new ArgumentNullException(nameof(config) , "ClientId is missing");
            _secret = config ["PayPalSettings:Secret"] ?? throw new ArgumentNullException(nameof(config) , "Secret is missing");
            _url = config ["PayPalSettings:Url"] ?? throw new ArgumentNullException(nameof(config) , "Url is missing");
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("SendClientId")]
        public IActionResult SendClientId ()
        {
            if ( string.IsNullOrEmpty(_clientId) )
            {
                return BadRequest("ClientId is missing");
            }

            return Ok(_clientId);
        }

        [HttpPost("CheckoutPayPal")]
        public async Task<IActionResult> CheckoutPayPal ([FromBody] double? amount)
        {
            if ( amount == null )
            {
                return BadRequest("Amount cannot be null");
            }

            var checkoutData = CreateCheckoutPayload(amount.Value);
            string accessToken = await GetAccessToken();

            if ( string.IsNullOrEmpty(accessToken) )
            {
                return Unauthorized("Failed to retrieve access token");
            }

            string checkoutUrl = $"{_url}/v2/checkout/orders";
            var result = await SendCheckoutRequest(checkoutUrl , checkoutData , accessToken);

            return result ?? NotFound("Failed to retrieve PayPal order ID");
        }

        private JsonObject CreateCheckoutPayload (double amount)
        {
            var purchaseUnit = new JsonObject
            {
                { "value", amount }
            };

            var purchaseUnits = new JsonArray { purchaseUnit };

            return new JsonObject
            {
                { "intent", "CAPTURE" },
                { "purchase_units", purchaseUnits }
            };
        }



        private async Task<IActionResult?> SendCheckoutRequest (string url , JsonObject payload , string accessToken)
        {
            using var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization" , $"Bearer {accessToken}");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post , url)
            {
                Content = new StringContent(payload.ToString() , Encoding.UTF8 , "application/json")
            };

            var response = await client.SendAsync(requestMessage);
            if ( !response.IsSuccessStatusCode ) return null;

            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonNode.Parse(responseString);

            if ( jsonResponse == null ) return null;

            string paypalOrderId = jsonResponse ["id"]?.ToString() ?? string.Empty;
            return Ok(paypalOrderId);
        }
        [HttpPost("CheckoutCard")]
        public async Task<IActionResult> CheckoutCard ([FromBody] CardPaymentRequest cardPaymentRequest)
        {
            if ( cardPaymentRequest == null || cardPaymentRequest.Amount <= 0 )
            {
                return BadRequest("Invalid card payment request");
            }

            string accessToken = await GetAccessToken();
            if ( string.IsNullOrEmpty(accessToken) )
            {
                return Unauthorized("Failed to retrieve access token");
            }

            var checkoutData = CreateCardPaymentPayload(cardPaymentRequest);
            string checkoutUrl = $"{_url}/v2/checkout/orders";
            var result = await SendCheckoutRequest(checkoutUrl , checkoutData , accessToken);

            return result ?? NotFound("Failed to process card payment");
        }
        private JsonObject CreateCardPaymentPayload (CardPaymentRequest request)
        {
            var cardDetails = new JsonObject
            {
                { "number", request.CardNumber },
                { "expiry", request.Expiry },
                { "cvv", request.CVV },
                { "name", request.CardHolderName }
            };

            var purchaseUnit = new JsonObject
            {
                { "amount", new JsonObject { { "currency_code", "USD" }, { "value", request.Amount } } },
                { "payment_method", new JsonObject { { "type", "CARD" }, { "card", cardDetails } } }
            };

            var purchaseUnits = new JsonArray { purchaseUnit };

            return new JsonObject
            {
                { "intent", "CAPTURE" },
                { "purchase_units", purchaseUnits }
            };
        }

        protected async Task<string> GetAccessToken ()
        {
            string requestUrl = $"{_url}/v1/oauth2/token";
            string accessToken = string.Empty;

            try
            {
                using var client = _httpClientFactory.CreateClient();
                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_secret}"));
                client.DefaultRequestHeaders.Add("Authorization" , $"Basic {credentials}");

                var requestMessage = new HttpRequestMessage(HttpMethod.Post , requestUrl)
                {
                    Content = new StringContent("grant_type=client_credentials" , Encoding.UTF8 , "application/x-www-form-urlencoded")
                };

                var response = await client.SendAsync(requestMessage);
                if ( response.IsSuccessStatusCode )
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(responseString);
                    accessToken = jsonResponse? ["access_token"]?.ToString() ?? string.Empty;
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine($"Error fetching access token: {ex.Message}");
            }

            return accessToken;
        }
        public class CardPaymentRequest
        {
            public string CardNumber { get; set; }
            public string Expiry { get; set; }
            public string CVV { get; set; }
            public string CardHolderName { get; set; }
            public double Amount { get; set; }
        }
    }
}
