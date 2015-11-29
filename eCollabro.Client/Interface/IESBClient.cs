#region References

using eCollabro.Client.Models.ESB;
using eCollabro.Client.Models.Core;
using System.Collections.Generic;

#endregion

namespace eCollabro.Client.Interface
{
    /// <summary>
    /// IADPClient
    /// </summary>
    public interface IESBClient:IBaseClient
    {

        /// <summary>
        /// GetESBApps
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        List<ESBAppModel> GetESBApps(int serviceId);

    }
}
