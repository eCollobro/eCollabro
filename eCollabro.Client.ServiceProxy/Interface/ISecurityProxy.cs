using eCollabro.Service.DataContracts;
using eCollabro.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.Client.ServiceProxy.Interface
{
    public interface ISecurityProxy
    {
        #region Methods

        void Initialize(UserContextDC activeUserContext);

        /// <summary>
        /// Execute
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="serviceOperation"></param>
        /// <returns></returns>
        ServiceResponse<TResponse> Execute<TResponse>(Func<ISecurityService, ServiceResponse<TResponse>> serviceOperation);

        /// <summary>
        /// Execute
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="serviceOperation"></param>
        /// <returns></returns>
        ServiceResponse Execute(Func<ISecurityService, ServiceResponse> serviceOperation);

        #endregion
    }
}
