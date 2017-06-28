using Demo02.Lib.ApiModels.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo02.AppFilters
{
    public class AppExceptionFilter : ExceptionFilterAttribute
    {
	    public override void OnException(ExceptionContext context)
	    {
		    var result = new ResultError
		    {
			    Errors = new SerializableError(context.ModelState),
			    Message = context.Exception.Message
		    };

		    var resultObj = new ObjectResult(result);

		    context.Result = resultObj;
	    }
    }
}
