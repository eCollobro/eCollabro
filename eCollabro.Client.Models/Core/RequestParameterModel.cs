// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>

namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// RequestContextParameter
    /// </summary>
    public class RequestContextParameter
    {
        public int Draw { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderByColumn { get; set; }
        public string OrderByDirection { get; set; }
        public string KeywordSearch { get; set; }
    }

    /// <summary>
    /// ResponseContextParameter
    /// </summary>
    public class ResponseContextParameter
    {
        public int NumberOfRecords { get; set; }
    }

    /// <summary>
    /// UserContextModel
    /// </summary>
    public class UserContextModel
    {
        public string UserName { get; set; }
        public string DomainName { get; set; }
        public string Language { get; set; }
        public int SiteId { get; set; }

    }
}
