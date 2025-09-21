using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CrmApp.Server.Filters
{
    public class ModelValidationThrowFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var failures = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .SelectMany(kvp => kvp.Value!.Errors.Select(e =>
                        new FluentValidation.Results.ValidationFailure(kvp.Key, e.ErrorMessage)))
                    .ToList();

                throw new ValidationException(failures);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
