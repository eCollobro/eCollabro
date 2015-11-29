#region References

using eCollabro.Client.Models.Core;
using System.Collections.Generic;

#endregion

namespace eCollabro.Client.Interface
{
    /// <summary>
    /// ILookupClient
    /// </summary>
    public interface ILookupClient
    {

        /// <summary>
        /// GetNavigationTypes
        /// </summary>
        /// <returns></returns>
        List<NavigationTypeModel> GetNavigationTypes();


        /// <summary>
        /// GetLanguages
        /// </summary>
        /// <returns></returns>
        List<LanguageModel> GetLanguages();
    }
}
