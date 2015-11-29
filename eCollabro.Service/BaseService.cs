// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using eCollabro.BAL;
using eCollabro.BAL.Entities.Models;
using eCollabro.Common;
using eCollabro.Resources;
using eCollabro.Exceptions;
using eCollabro.Service.DataContracts;
using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using eCollabro.DataMapper;
using log4net;
using System.ServiceModel;

#endregion 

namespace eCollabro.Service
{
    /// <summary>
    /// BaseService
    /// </summary>
    public abstract class BaseService:IDisposable
    {
        #region Data Members
        protected readonly ILog log = null;
        protected ResourceManager _coreValidationResourceManager = null;
        private bool _disposed;

        #endregion 

        public BaseService()
        {
           log= LogManager.GetLogger(this.GetType());
        }

        #region Methods

        /// <summary>
        /// SetUserContext
        /// </summary>
        /// <param name="userContext"></param>
        protected void SetContext()
        {
            CommonManager commonManager = new CommonManager();
            UserContextDC userContextDC= OperationContext.Current.IncomingMessageHeaders.GetHeader<UserContextDC>("ActiveUser", "s");
            UserContext userContext = Mapper.Map<UserContextDC, UserContext>(userContextDC);
            RequestContextParameterDC requestContextParameterDC = null; // OperationContext.Current.IncomingMessageHeaders.GetHeader<RequestContextParameterDC>("RequestContext", "s");
            RequestContextParameter requestContextParameter=null;
            if (requestContextParameterDC != null)
                requestContextParameter = Mapper.Map<RequestContextParameterDC, RequestContextParameter>(requestContextParameterDC);
            else
                requestContextParameter = new RequestContextParameter();
            commonManager.SetUserContext(userContext,requestContextParameter);
            _coreValidationResourceManager = new ResourceManager(typeof(CoreValidationMessages));

        }
        
        /// <summary>
        /// HandleError
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="response"></param>
        protected void HandleError(Exception ex,IBaseServiceResponse response)
        {
            string errorCode=string.Empty;
            if (ex.GetType() == typeof(BusinessException))
            {
                errorCode = (ex as BusinessException).Code;
                response.Status = ResponseStatus.BusinessException;
            }
            else
            {
                response.Status = ResponseStatus.Exception;
                // log error 
                log.Error(ex.Message,ex);
            }
            response.ResponseMessage.MessageCode= errorCode;
            response.ResponseMessage.Message=ex.Message;
            
        }

        /// <summary>
        /// SetLanguage
        /// </summary>
        /// <param name="language"></param>
        protected void SetLanguage(string language)
        {
            CultureInfo cl = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentCulture = cl;
            Thread.CurrentThread.CurrentUICulture = cl;
        }
        
 

        /// <summary>
        /// ValidateRequest
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        protected void ValidateRequest(BaseServiceRequest request, BaseServiceResponse response)
        {
            if (request == null)
                response.ResponseMessage.Errors.Add(new ErrorDC { ErrorCode = CoreValidationMessagesConstants.InvalidArguments ,ErrorMessage= string.Format(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidArguments), "[Request user context]")});
            else if (request.UserContext.Language == null)
                response.ResponseMessage.Errors.Add(new ErrorDC { ErrorCode = CoreValidationMessagesConstants.InvalidArguments, ErrorMessage = string.Format(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidArguments),"[Request user context language]") });
            if(response.ResponseMessage.Errors.Count>0)
            {
                response.ResponseMessage.MessageCode = CoreValidationMessagesConstants.InvalidArguments;
                response.ResponseMessage.Message = _coreValidationResourceManager.GetString(CoreValidationMessagesConstants.InvalidArguments);
                response.Status = ResponseStatus.BusinessException;
            }
            if (!response.Status.Equals(ResponseStatus.BusinessException))
            {
                SetLanguage(request.UserContext.Language);
                SetContext();
            }

        }

        /// <summary>
        /// Dispose()
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                // to do 
            }
            _disposed = true;
        }

        #endregion
    }
}
