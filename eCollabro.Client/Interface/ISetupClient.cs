#region References
using System.Collections.Generic;
using eCollabro.Client.Models.Core;
#endregion

namespace eCollabro.Client.Interface
{
    /// <summary>
    /// ISetupClient
    /// </summary>
    public interface ISetupClient
    {
        /// <summary>
        /// CheckEcollabroSetup
        /// </summary>
        /// <returns></returns>
        bool CheckEcollabroSetup();


        /// <summary>
        /// eCollabroSetup
        /// </summary>
        /// <param name="registerModel"></param>
        void eCollabroSetup(RegisterModel registerModel);


         /// <summary>
        /// GetEmailConfiguration
        /// </summary>
        /// <returns></returns>
        EmailConfigurationModel GetEmailConfiguration();

         /// <summary>
        /// SaveEmailConfiguration
        /// </summary>
        /// <param name="emailConfigurationModel"></param>
        void SaveEmailConfiguration(EmailConfigurationModel emailConfigurationModel);

        #region Site Collection Admin

        /// <summary>
        /// GetSiteCollectionAdmins
        /// </summary>
        /// <returns></returns>
        List<SiteCollectionAdminModel> GetSiteCollectionAdmins();

        /// <summary>
        /// SaveSiteCollectionAdmin
        /// </summary>
        /// <param name="siteModel"></param>
        /// <returns></returns>
        void SaveSiteCollectionAdmin(SiteCollectionAdminModel siteCollectionAdminModel);

        /// <summary>
        /// DeleteSiteCollectionAdmin
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        void DeleteSiteCollectionAdmin(int userId);

        #endregion

    }
}
