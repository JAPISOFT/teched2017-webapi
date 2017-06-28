using System;
using System.Net;
using Demo02.Lib.ApiModels.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Demo02.Controllers
{
	public abstract class ApiController : ControllerBase
	{
		public override OkObjectResult Ok(object value)
		{
			var result = new ResultObject
			{
				Status = HttpStatusCode.OK,
				Result = value
			};

			return base.Ok(result);
		}

		public new OkObjectResult Ok()
		{
			var result = new ResultBase()
			{
				Status = HttpStatusCode.OK
			};

			return base.Ok(result);
		}

		public override CreatedAtRouteResult CreatedAtRoute(string routeName, object routeValues, object value)
		{
			var responseData = new ResultObject()
			{
				Status = HttpStatusCode.Created,
				Result = value
			};

			return base.CreatedAtRoute(routeName, routeValues, responseData);
		}

		public new NotFoundObjectResult NotFound()
		{
			var responseData = new ResultError()
			{
				Status = HttpStatusCode.Created,
			};

			return base.NotFound(responseData);
		}

		public NotFoundObjectResult NotFound(string message)
		{
			var responseData = new ResultError()
			{
				Status = HttpStatusCode.Created,
				Message = message
			};

			return base.NotFound(responseData);
		}

		public override BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
		{
			if (modelState == null)
				throw new ArgumentNullException(nameof(modelState));

			var responseData = new ResultError()
			{
				Status = HttpStatusCode.BadRequest,
				Errors = new SerializableError(modelState),
				Message = "See errors for more details"
			};

			return base.BadRequest(responseData);
		}

		public override BadRequestObjectResult BadRequest(object errorMessage)
		{
			var responseData = new ResultError()
			{
				Status = HttpStatusCode.BadRequest,
				Message = errorMessage.ToString()
			};

			return base.BadRequest(responseData);
		}
	}
}