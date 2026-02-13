using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CS_project_MVC_Classwork_1B.MiddleWare
{
    public class RequestAmountHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly PeriodicTimer _timer;

        // per-IP request counters
        private static readonly ConcurrentDictionary<string, int> _requestCounts = new();

        // time window and limit
        private const int Limit = 10;

        public RequestAmountHandlerMiddleWare(RequestDelegate next)
        {
            _next = next;
            // Interval timer set to  60 seconds
            _timer = new PeriodicTimer(TimeSpan.FromSeconds(60));

            //We used this thing on lesson with Pagination (sorting)
            //, but still i can't say what it does with my words
            _ = Task.Run(RunTimerAsync);
        }

        public async Task HandlerAmount(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            // increment count for this IP
            var count = _requestCounts.AddOrUpdate(ip, 1, (_, old) => old + 1);

            if (count > Limit)
            {
                context.Response.StatusCode = 249;
                await context.Response.WriteAsync("Too many requests. Try again later.");
                return;
            }

            await _next(context);
        }

        private async Task RunTimerAsync()
        {
            try
            {
                while (await _timer.WaitForNextTickAsync())
                {
                    // clear counts every interval (60 seconds)
                    _requestCounts.Clear();
                }
            }
            catch (OperationCanceledException)
            {
                //Idk what to write here  , i forget how to use in ASP exceptions and used agent ,
                //but he didn't described what he ever do.
                //Maybe when i will have a lot of time i will reread this code and try to add something here
            }
        }
    }
}
