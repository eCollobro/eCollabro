#region References
using System.Collections.Generic;
using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.Core;
using eCollabro.Client.ServiceProxy;
using eCollabro.Client.Models.Core;
using eCollabro.Client.Interface;
using eCollabro.DataMapper;
using eCollabro.Client.ServiceProxy.Interface;
#endregion

namespace eCollabro.Client
{
    /// <summary>
    /// LookupClient
    /// </summary>
    public class LookupClient : BaseClient, ILookupClient
    {
        private ILookupProxy _lookupProxy = null;

        public LookupClient()
        {
            _lookupProxy = new LookupServiceProxy();
            _lookupProxy.Initialize(SecurityClientTranslate.Convert(UserContext));
        }

        #region Methods

        /// <summary>
        /// GetNavigationTypes
        /// </summary>
        /// <returns></returns>
        public List<NavigationTypeModel> GetNavigationTypes()
        {
            List<NavigationTypeModel> navigationTypes = new List<NavigationTypeModel>();
            ServiceResponse<List<NavigationTypeDC>> NavigationTypesResponse = _lookupProxy.Execute(opt => opt.GetNavigationTypes());

            if (NavigationTypesResponse.Status == ResponseStatus.Success)
            {
                foreach (NavigationTypeDC navigationType in NavigationTypesResponse.Result)
                {
                    navigationTypes.Add(Mapper.Map<NavigationTypeDC, NavigationTypeModel>(navigationType));
                }
            }
            else
            {
                HandleError(NavigationTypesResponse.Status, NavigationTypesResponse.ResponseMessage);
            }
            return navigationTypes;
        }


        /// <summary>
        /// GetLanguages
        /// </summary>
        /// <returns></returns>
        public List<LanguageModel> GetLanguages()
        {
            List<LanguageModel> languages = new List<LanguageModel>();
            ServiceResponse<List<LanguageDC>> LanguagesResponse = _lookupProxy.Execute(opt => opt.GetLanguages());
            if (LanguagesResponse.Status == ResponseStatus.Success)
            {
                foreach (LanguageDC language in LanguagesResponse.Result)
                {
                    languages.Add(Mapper.Map<LanguageDC, LanguageModel>(language));
                }
            }
            else
            {
                HandleError(LanguagesResponse.Status, LanguagesResponse.ResponseMessage);
            }
            return languages;
        }



        #endregion
    }
}
