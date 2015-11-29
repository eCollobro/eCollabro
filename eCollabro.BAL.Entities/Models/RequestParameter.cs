#region References
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion 

namespace eCollabro.BAL.Entities.Models
{

    public class RequestContextParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderByColumn { get; set; }
        public string OrderByDirection { get; set; }
        public string KeywordSearch { get; set; }
    }

    public class ResponseContextParameter
    {
        public int NumberOfRecords { get; set; }
    }

    /// <summary>
    /// UserContext
    /// </summary>
    public class UserContext
    {
        #region Properties

        /// <summary>
        /// UserId
        /// </summary>
        public int UserId {get;set;}
        
        /// <summary>
        /// LanguageId
        /// </summary>
        public int LanguageId{get;set;}

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Language
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// SiteId
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// ImpersonateViewRights
        /// </summary>
        public bool ImpersonateViewRights { get; set; }


        #endregion 
    }
}
