using System.Diagnostics.CodeAnalysis;
using Exception;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MovieStreamingServiceApi.Controller
{
    public class ModelStateActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var errors = new List<object>();
                foreach(var model in context.ModelState.Values)
                {
                    foreach(var error in model.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                throw new BaseCustomException(400,"Bad request","Please check the input data",errors);
                      
            }
        }

        [ExcludeFromCodeCoverage]
        public void OnActionExecuting(ActionExecutingContext context)
        {
        
        }
    }
}