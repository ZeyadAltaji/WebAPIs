namespace WebAPIs.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _keyToken;

        public TokenMiddleware (RequestDelegate next , IConfiguration configuration)
        {
            _next = next;
            _keyToken = configuration ["AppSettings:Key"];
        }

        public async Task InvokeAsync (HttpContext context)
        {
            if ( !context.Request.Headers.TryGetValue("KeyToken" , out var extractedKeyToken) )
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Key Token is missing.");
                return;
            }

            if ( !string.Equals(_keyToken , extractedKeyToken) )
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Key Token.");
                return;
            }

            await _next(context);
        }
    }
}
