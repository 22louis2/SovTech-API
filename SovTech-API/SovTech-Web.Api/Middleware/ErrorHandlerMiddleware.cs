using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using SovTech_Web.Domain.Commons;
using SovTech_Web.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SovTech_Web.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// ErrorHandlerMiddleware constructor
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke method that determines what error message to be returned through the middleware pipeline
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    AppException => (int)HttpStatusCode.BadRequest, // custom application error
                    KeyNotFoundException => (int)HttpStatusCode.NotFound, // not found error
                    _ => (int)HttpStatusCode.InternalServerError, // unhandled error
                };

                var modelDictionary = new ModelStateDictionary();
                modelDictionary.AddModelError(error?.Source, error?.Message);
                var result = JsonConvert.SerializeObject(ResponseHelper.BuildResponse(false, error?.InnerException?.ToString(), modelDictionary, ""));
                await response.WriteAsync(result);
            }
        }
    }
}
