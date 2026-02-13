namespace CS_project_MVC_Classwork_1B.MiddleWare
{
    public class RequestLoggingMiddleWare
    {
        private readonly RequestDelegate _next;
        public RequestLoggingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;

            Console.WriteLine($"[{DateTime.Now}] -  {method} - {path}");

            await _next(context);

            var statusCode = context.Response.StatusCode;
            Console.WriteLine($"Response: {statusCode}");
        }
    }
}
