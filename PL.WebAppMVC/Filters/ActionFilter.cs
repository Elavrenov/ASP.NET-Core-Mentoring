using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace PL.WebAppMVC.Filters
{
    public class ActionFilter : IAsyncActionFilter
    {
        private readonly string _value;
        private readonly ILogger<ActionFilter> _logger;

        public ActionFilter(string value, ILogger<ActionFilter> logger)
        {
            _logger = logger;
            _value = value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"Action {_value} starting");
            await next();
            _logger.LogInformation($"Started previously action: '{_value}' was ended");
        }
    }
}