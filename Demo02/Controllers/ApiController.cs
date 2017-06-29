using System;
using System.Net;
using Demo02.Lib.ApiModels.Base;
using Demo02.Lib.ApiModels.Base.Hateoas;
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

			result.AddApiLinks(Url);

			return base.Ok(result);
		}

		public new OkObjectResult Ok()
		{
			var result = new ResultBase
			{
				Status = HttpStatusCode.OK
			};

			return base.Ok(result);
		}

		public override CreatedAtRouteResult CreatedAtRoute(string routeName, object routeValues, object value)
		{
			var result = new ResultObject
			{
				Status = HttpStatusCode.Created,
				Result = value
			};

			result.AddApiLinks(Url);

			return base.CreatedAtRoute(routeName, routeValues, result);
		}

		public new NotFoundObjectResult NotFound()
		{
			var result = new ResultError
			{
				Status = HttpStatusCode.Created,
			};

			return base.NotFound(result);
		}

		public NotFoundObjectResult NotFound(string message)
		{
			var result = new ResultError
			{
				Status = HttpStatusCode.Created,
				Message = message
			};

			return base.NotFound(result);
		}

		public override BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
		{
			if (modelState == null)
			{
				throw new ArgumentNullException(nameof(modelState));
			}

			var result = new ResultError()
			{
				Status = HttpStatusCode.BadRequest,
				Errors = new SerializableError(modelState),
				Message = "See errors for more details"
			};

			return base.BadRequest(result);
		}

		public override BadRequestObjectResult BadRequest(object errorMessage)
		{
			var result = new ResultError
			{
				Status = HttpStatusCode.BadRequest,
				Message = errorMessage.ToString()
			};

			return base.BadRequest(result);
		}
	}

	public static class HateoasCommonExtensions
	{
		public static ResultObject AddApiLinks(this ResultObject resultObject, IUrlHelper urlHelper)
		{
			resultObject.Links.Add(
				new Link
				{
					Href = urlHelper.Link("GetProducts", null),
					Rel = "products",
					Description = "Seznam produktů",
					Method = "GET"
				}
			);

			return resultObject;
		}
	}
}