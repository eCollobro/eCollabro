#region References
using eCollabro.DataMapper;
using System.Collections.Generic;
using eCollabro.Service.DataContracts;
using eCollabro.Client.ServiceProxy;
using eCollabro.Service.DataContracts.Core;
using eCollabro.Utilities;
using eCollabro.Client.Models.Core;
using eCollabro.Client.Interface;
using eCollabro.Client.ServiceProxy.Interface;
#endregion

namespace eCollabro.Client
{
    /// <summary>
    /// SetupClient
    /// </summary>
    public class SetupClient : BaseClient, ISetupClient
    {
        private ISetupProxy _setupProxy = null;

        public SetupClient()
        {
            _setupProxy = new SetupServiceProxy();
            _setupProxy.Initialize(SecurityClientTranslate.Convert(UserContext));
        }


        #region eCollabro Setup

        /// <summary>
        /// CheckEcollabroSetup
        /// </summary>
        /// <returns></returns>
        public bool CheckEcollabroSetup()
        {
            bool setupDone = false;
            ServiceResponse<bool> checkEcollabroSetupResponse = _setupProxy.Execute(opt => opt.CheckEcollabroSetup());
            if (checkEcollabroSetupResponse.Status != ResponseStatus.Success)
            {
                HandleError(checkEcollabroSetupResponse.Status, checkEcollabroSetupResponse.ResponseMessage);
            }
            else
            {
                setupDone = checkEcollabroSetupResponse.Result;
            }
            return setupDone;
        }

        /// <summary>
        /// eCollabroSetup
        /// </summary>
        /// <param name="registerModel"></param>
        public void eCollabroSetup(RegisterModel registerModel)
        {
            RegisterDC registerDC = new RegisterDC();
            registerDC.UserName = registerModel.UserName;
            registerDC.Password = DataEncryption.Encrypt(registerModel.Password);
            registerDC.Email = registerModel.Email;
            ServiceResponse eCollabroSetupResponse = _setupProxy.Execute(opt => opt.eCollabroSetup(registerDC));
            if (eCollabroSetupResponse.Status != ResponseStatus.Success)
            {
                HandleError(eCollabroSetupResponse.Status, eCollabroSetupResponse.ResponseMessage);
            }
        }

        #endregion

        #region Email Configuration

        /// <summary>
        /// GetEmailConfiguration
        /// </summary>
        /// <returns></returns>
        public EmailConfigurationModel GetEmailConfiguration()
        {
            EmailConfigurationModel emailConfiguration = null;
            ServiceResponse<EmailConfigurationDC> emailConfigurationResponse = _setupProxy.Execute(opt => opt.GetEmailConfiguration());
            if (emailConfigurationResponse.Status != ResponseStatus.Success)
            {
                HandleError(emailConfigurationResponse.Status, emailConfigurationResponse.ResponseMessage);
            }
            else
            {
                emailConfiguration = Mapper.Map<EmailConfigurationDC, EmailConfigurationModel>(emailConfigurationResponse.Result);
            }
            return emailConfiguration;
        }


        /// <summary>
        /// SaveEmailConfiguration
        /// </summary>
        /// <param name="emailConfigurationModel"></param>
        public void SaveEmailConfiguration(EmailConfigurationModel emailConfigurationModel)
        {
            EmailConfigurationDC emailConfigurationDC = Mapper.Map<EmailConfigurationModel, EmailConfigurationDC>(emailConfigurationModel);
            ServiceResponse emailConfigurationResponse = _setupProxy.Execute(opt => opt.SaveEmailConfiguration(emailConfigurationDC));
            if (emailConfigurationResponse.Status != ResponseStatus.Success)
            {
                HandleError(emailConfigurationResponse.Status, emailConfigurationResponse.ResponseMessage);
            }
        }

        #endregion

        #region Site Collection Admin

        /// <summary>
        /// GetSiteCollectionAdmins
        /// </summary>
        /// <returns></returns>
        public List<SiteCollectionAdminModel> GetSiteCollectionAdmins()
        {
            List<SiteCollectionAdminModel> siteCollectionAdminModels = new List<SiteCollectionAdminModel>();
            ServiceResponse<List<SiteCollectionAdminDC>> userResponse = _setupProxy.Execute(opt => opt.GetSiteCollectionAdmins());

            if (userResponse.Status == ResponseStatus.Success)
            {
                foreach (SiteCollectionAdminDC siteCollectionAdminDC in userResponse.Result)
                {
                    siteCollectionAdminModels.Add(Mapper.Map<SiteCollectionAdminDC, SiteCollectionAdminModel>(siteCollectionAdminDC));
                }
            }
            else
            {
                HandleError(userResponse.Status, userResponse.ResponseMessage);
            }
            return siteCollectionAdminModels;
        }

        /// <summary>
        /// SaveSite
        /// </summary>
        /// <param name="siteModel"></param>
        /// <returns></returns>
        public void SaveSiteCollectionAdmin(SiteCollectionAdminModel siteCollectionAdminModel)
        {
            ServiceResponse siteCollectionAdminResponse = _setupProxy.Execute(opt => opt.SaveSiteCollectionAdmin(siteCollectionAdminModel.UserName));
            if (siteCollectionAdminResponse.Status != ResponseStatus.Success)
                HandleError(siteCollectionAdminResponse.Status, siteCollectionAdminResponse.ResponseMessage);
        }

        /// <summary>
        /// DeleteSiteCollectionAdmin
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public void DeleteSiteCollectionAdmin(int userId)
        {
            ServiceResponse siteCollectionAdminResponse = _setupProxy.Execute(opt => opt.DeleteSiteCollectionAdmin(userId));

            if (siteCollectionAdminResponse.Status != ResponseStatus.Success)
                HandleError(siteCollectionAdminResponse.Status, siteCollectionAdminResponse.ResponseMessage);
        }

        #endregion

    }
}
