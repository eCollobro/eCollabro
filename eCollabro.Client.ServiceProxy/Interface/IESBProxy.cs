using eCollabro.Service.DataContracts;
using eCollabro.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.Client.ServiceProxy.Interface
{
    public interface IESBProxy
    {
        #region Methods

        void Initialize(UserContextDC activeUserContext);

        /// <summary>
        /// Execute
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="serviceOperation"></param>
        /// <returns></returns>
        ServiceResponse<TResponse> Execute<TResponse>(Func<IESBService, ServiceResponse<TResponse>> serviceOperation);

        /// <summary>
        /// Execute
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="serviceOperation"></param>
        /// <returns></returns>
        ServiceResponse Execute(Func<IESBService, ServiceResponse> serviceOperation);

        #endregion
    }
}
