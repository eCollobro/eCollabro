using eCollabro.Client.Models.Core;

namespace eCollabro.Client.Interface
{
    public interface IBaseClient
    {
        #region Data Member

        /// <summary>
        /// UserContext
        /// </summary>
        UserContextModel UserContext { get; }

        /// <summary>
        /// RequestContext
        /// </summary>
        RequestContextParameter RequestContext { get; set; }
        
        /// <summary>
        /// ResponseContext
        /// </summary>
        ResponseContextParameter ResponseContext { get; set; }


        #endregion 
    }
}
