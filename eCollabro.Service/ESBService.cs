// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Service.DataContracts;
using System;
using System.Collections.Generic;
using eCollabro.DataMapper;
using eCollabro.Service.ServiceContracts;
using eCollabro.Service.DataContracts.ADP;

#endregion

namespace eCollabro.Service
{
    /// <summary>
    /// ADPService
    /// </summary>
    public class ESBService : BaseService, IESBService
    {
        #region Data Members

        //private ESBManager _esbManager;

        #endregion

        #region Constructor

        public ESBService()
        {
           // _esbManager = new ESBManager();
        }

        #endregion

        #region Methods


        /// <summary>
        /// GetESBApps
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public ServiceResponse<List<ESBAppDC>> GetESBApps(int serviceId)
        {
            ServiceResponse<List<ESBAppDC>> esbAppResponse = new ServiceResponse<List<ESBAppDC>>();
            try
            {
                SetContext();
               // List<AppEntity> esbApps = _esbManager.GetAllApps(serviceId);
                esbAppResponse.Result=new List<ESBAppDC>();
               // foreach(AppEntity appEntity in esbApps)
                //{
                //   esbAppResponse.Result.Add(Mapper.Map<AppEntity, ESBAppDC>(appEntity));
                //}
            }
            catch (Exception ex)
            {
                HandleError(ex, esbAppResponse);
            }

            return esbAppResponse;

        }

        #endregion
    }
}
