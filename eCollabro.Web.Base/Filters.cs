// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

#endregion 

namespace eCollabro.Web.Base
{
    /// <summary>
    /// WebApiExceptionFiler
    /// </summary>
    public class WebApiExceptionFiler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception.GetType() == typeof(BusinessException))
            {
                BusinessException businessException=context.Exception as BusinessException;
                var errorModel = new { ExceptionMessage = businessException.Message, ExceptionCode = businessException.Code, ExceptionOveridable = false };
                context.Response = context.Request.CreateResponse(HttpStatusCode.ExpectationFailed, errorModel);
            }
            else
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, context.Exception);
            }
        }
    }

   
}
