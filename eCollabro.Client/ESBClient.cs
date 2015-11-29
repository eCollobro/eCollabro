#region References
using System.Collections.Generic;
using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.Core;
using eCollabro.Client.ServiceProxy;
using eCollabro.Client.Models.Core;
using eCollabro.Client.Interface;
using eCollabro.DataMapper;
using eCollabro.Client.ServiceProxy.Interface;
using eCollabro.Client.Models.ESB;
using eCollabro.Service.DataContracts.ADP;
#endregion

namespace eCollabro.Client
{
    /// <summary>
    /// ADPClient
    /// </summary>
    public class ESBClient : BaseClient, IESBClient
    {
        private IESBProxy _adpProxy = null;

        public ESBClient()
        {
            _adpProxy = new ESBServiceProxy();
            _adpProxy.Initialize(SecurityClientTranslate.Convert(UserContext));
        }

        #region Methods


        /// <summary>
        /// GetESBApps
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public List<ESBAppModel> GetESBApps(int serviceId)
        {
            List<ESBAppModel> esbApps = new List<ESBAppModel>();
            ServiceResponse<List<ESBAppDC>> esbAppsResponse = _adpProxy.Execute(opt => opt.GetESBApps(serviceId));
            if (esbAppsResponse.Status == ResponseStatus.Success)
            {
                foreach(ESBAppDC esbAppDC in esbAppsResponse.Result)
                {
                 esbApps.Add(Mapper.Map<ESBAppDC,ESBAppModel>(esbAppDC));
                }
            }
            else
            {
                HandleError(esbAppsResponse.Status, esbAppsResponse.ResponseMessage);
            }
            return esbApps;
        }
  

        #endregion
    }
}
