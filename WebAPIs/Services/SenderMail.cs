using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace WebAPIs.Services
{
    public class SenderMail
    {
        private readonly IConfiguration configuration;
        public SenderMail (IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void sendmail (string SenderEmail , string SenderName , string receivermail , string receivername , string subject , string message , string htmlContent = null)
        {
            Configuration.Default.ApiKey.Add("api-key" , configuration ["BrevoAPIs:APIKey"]);

            var apiInstance = new TransactionalEmailsApi();

            SendSmtpEmailSender sender = new SendSmtpEmailSender(SenderName , SenderEmail);

            SendSmtpEmailTo receiver1 = new SendSmtpEmailTo(receivermail , receivername);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(receiver1);

            string HtmlContent = htmlContent ?? $"<p>{message}</p>"; // Default HTML content if not provided
            string TextContent = message;

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(sender , To , null , null , HtmlContent , TextContent , subject);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Console.WriteLine("res" + result.ToJson());
            }
            catch ( ApiException e )
            {
                Console.WriteLine($"Exception when calling TransactionalEmailsApi.SendTransacEmail: {e.Message}");
                Console.WriteLine($"HTTP Response: {e.ErrorCode}");
                Console.WriteLine($"HTTP Response Body: {e.ErrorContent}");
            }
            catch ( Exception e )
            {
                Console.WriteLine($"General exception: {e.Message}");
            }
        }

    }
}

