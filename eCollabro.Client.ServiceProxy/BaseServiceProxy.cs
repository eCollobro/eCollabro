using eCollabro.Service.DataContracts;
using eCollabro.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.Client.ServiceProxy
{
    public abstract class BaseBusinessProxy<TServiceInterface> : IDisposable where TServiceInterface : class
    {

        #region Private Members

        private ChannelFactory<TServiceInterface> _factory = null;
        protected TServiceInterface _client = null;
        bool disposed = false;

        #endregion


        public virtual string EndpointServiceConfigurationName { get; set; }

        public void Initialize(UserContextDC activeUser)
        {
            eCollabroRequest.ActiveUser = activeUser;
        }

        protected TServiceInterface GetClient()
        {
            if (_factory == null)
            {
                _factory = new ChannelFactory<TServiceInterface>(EndpointServiceConfigurationName);
                _factory.Endpoint.EndpointBehaviors.Add(new eCollabroServiceBehavior());
            }
            _client = _factory.CreateChannel();
            
            return _client;
        }

        /// <summary>
        /// ExecuteOperation
        /// </summary>
        /// <param name="serviceOperation"></param>
        public ServiceResponse<TResponse> Execute<TResponse>(Func<TServiceInterface, ServiceResponse<TResponse>> serviceOperation)
        {
            ServiceResponse<TResponse> response;
            try
            {
                response = serviceOperation(GetClient());
            }
            catch (FaultException fex)
            {
                response = new ServiceResponse<TResponse>();
                response.Status = ResponseStatus.Exception;
                response.ResponseMessage = new ServiceResponseMessage();
                response.ResponseMessage.Message = fex.Message;
                //HandleException(serviceOperation.Method, fex.GetBaseException());
            }
            catch (Exception ex)
            {
                response = new ServiceResponse<TResponse>();
                response.Status = ResponseStatus.Exception;
                response.ResponseMessage = new ServiceResponseMessage();
                response.ResponseMessage.Message = ex.Message;
                //HandleException(serviceOperation.Method, ex.GetBaseException());
            }
            finally
            {
                CleanupClient();
            }
            return response;
        }

        /// <summary>
        /// ExecuteOperation
        /// </summary>
        /// <param name="serviceOperation"></param>
        public ServiceResponse Execute(Func<TServiceInterface, ServiceResponse> serviceOperation)
        {
            ServiceResponse response;
            try
            {
                response = serviceOperation(GetClient());
            }
            catch (FaultException fex)
            {
                response = new ServiceResponse();
                response.Status = ResponseStatus.Exception;
                response.ResponseMessage=new ServiceResponseMessage();
                response.ResponseMessage.Message=fex.Message;
                //HandleException(serviceOperation.Method, fex.GetBaseException());
            }
            catch (Exception ex)
            {
                response = new ServiceResponse();
                response.Status=ResponseStatus.Exception;
                response.ResponseMessage = new ServiceResponseMessage();
                response.ResponseMessage.Message= ex.Message;
                //HandleException(serviceOperation.Method, ex.GetBaseException());
            }
            finally
            {
                CleanupClient();
            }
            return response;
        }


        private void CleanupClient()
        {
            if (_client != null && (IClientChannel)_client != null)
            {
                if (((IClientChannel)_client).State == CommunicationState.Faulted) { ((IClientChannel)_client).Abort(); }
                ((IClientChannel)_client).Dispose();
            }
        }

        #region IDisposable Members_

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                try
                {
                    if (_factory != null)
                    {
                        if (_factory.State == CommunicationState.Faulted)
                        {
                            _factory.Abort();
                        }
                        _factory.Close();
                    }
                }
                finally
                {
                    _factory = null;
                }
            }

            disposed = true;
        }

        #endregion
    }
}
