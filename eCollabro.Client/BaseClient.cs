#region References
using eCollabro.Client.Models.Core;
using eCollabro.DataMapper;
using eCollabro.Exceptions;
using eCollabro.Service.DataContracts;
using System;
using System.Threading;

#endregion

namespace eCollabro.Client
{
    /// <summary>
    /// BaseClient
    /// </summary>
    public class BaseClient
    {
        public RequestContextParameter RequestContext { get; set; }
        public ResponseContextParameter ResponseContext { get; set; }

        UserContextModel _userContext;

        public BaseClient()
        {
            this.RequestContext = new RequestContextParameter();
            this.ResponseContext = new ResponseContextParameter();
        }

        public UserContextModel UserContext
        {
            get
            {
                if (_userContext==null)
                {
                   _userContext = new UserContextModel();
                     if (System.Configuration.ConfigurationManager.AppSettings["SiteId"] != null)
                       this._userContext.SiteId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SiteId"]);
                    _userContext.Language = Thread.CurrentThread.CurrentUICulture.Name;
                    _userContext.UserName = Thread.CurrentPrincipal.Identity.Name;
                }
                return _userContext;
            }

        }


        /// <summary>
        /// HandleError
        /// </summary>
        /// <param name="ex"></param>
        protected bool HandleError(Exception ex)
        {
            bool handled = true;
            // add exceptions for which error need not to throw
            if (ex.GetType() != typeof(BusinessException))
            {
                // log error 
                //ExceptionLogger.LogError(ex);

            }
            handled = false;
            return handled;
        }


        /// <summary>
        /// HandleError
        /// </summary>
        /// <param name="responseStatus"></param>
        protected void HandleError(ResponseStatus responseStatus, ServiceResponseMessage responseMessage)
        {

            if (responseStatus == ResponseStatus.BusinessException)
            {
               throw new BusinessException(responseMessage.Message, responseMessage.MessageCode, responseMessage.Overridable);
            }

            else if (responseStatus == ResponseStatus.Exception)
            {
               throw new SysException(responseMessage.Message);
            }
        }

        /// <summary>
        /// SetRequestParameter
        /// </summary>
        /// <param name="serviceRequest"></param>
        protected void SetContext()
        {
            //eCollabro.Service.se
            //serviceRequest.UserContext = Mapper.Map<UserContextModel, UserContextDC>(this.UserContext);
            //serviceRequest.RequestContextParameters=Mapper.Map<RequestContextParameter,RequestContextParameterDC>(this.RequestContext);
        }
    }
}
