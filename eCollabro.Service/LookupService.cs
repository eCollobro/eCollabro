// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.BAL;
using eCollabro.BAL.Entities;
using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using eCollabro.BAL.Entities.Models;
using eCollabro.DataMapper;
using eCollabro.Service.ServiceContracts;

#endregion
namespace eCollabro.Service
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    /// <summary>
    /// LookupService
    /// </summary>
    public class LookupService : BaseService, ILookupService
    {
        /// <summary>
        /// GetLanguages
        /// </summary>
        /// <param name="requestLanguages"></param>
        /// <returns></returns>
        public ServiceResponse<List<LanguageDC>> GetLanguages()
        {
            ServiceResponse<List<LanguageDC>> languagesResponse = new ServiceResponse<List<LanguageDC>>();
            try
            {
                SetContext();
                LookupManager lookupManager = new LookupManager();
                List<lkpLanguage> languages = lookupManager.GetLanguages();
                languagesResponse.Result = new List<LanguageDC>();
                foreach (lkpLanguage language in languages)
                {
                    languagesResponse.Result.Add(Mapper.Map<lkpLanguage, LanguageDC>(language));
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, languagesResponse);
            }
            return languagesResponse;
        }

        /// <summary>
        /// GetNavigationTypes
        /// </summary>
        /// <param name="requestNavigationTypes"></param>
        /// <returns></returns>
        public ServiceResponse<List<NavigationTypeDC>> GetNavigationTypes()
        {
            ServiceResponse<List<NavigationTypeDC>> navigationTypesResponse = new ServiceResponse<List<NavigationTypeDC>>();
                try
                {
                    SetContext();
                    LookupManager lookupManager = new LookupManager();
                    List<lkpNavigationType> navigationTypes = lookupManager.GetNavigationTypes();
                    navigationTypesResponse.Result = new List<NavigationTypeDC>();
                    foreach (lkpNavigationType navigationType in navigationTypes)
                    {
                        navigationTypesResponse.Result.Add(Mapper.Map<lkpNavigationType, NavigationTypeDC>(navigationType));
                    }
                }
                catch (Exception ex)
                {
                    HandleError(ex, navigationTypesResponse);
                }
            return navigationTypesResponse;
        }

    }
}
