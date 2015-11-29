// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts
{
        /// <summary>
        /// RequestContextParameterDC
        /// </summary>
        [DataContract]
        public class RequestContextParameterDC
        {
            [DataMember]
            public int PageNumber { get; set; }

            [DataMember]
            public int PageSize { get; set; }

            [DataMember]
            public string OrderByColumn { get; set; }

            [DataMember]
            public string OrderByDirection { get; set; }

            [DataMember]
            public string KeywordSearch { get; set; }
        }

        [DataContract]
        public class ResponseContextParameterDC
        {
            [DataMember]
            public int NumberOfRecords { get; set; }
        }

        /// <summary>
        /// UserContextDC
        /// </summary>
        [DataContract]
        public class UserContextDC
        {
            /// <summary>
            /// UserName
            /// </summary>
            [DataMember]
            public string UserName { get; set; }

            /// <summary>
            /// Language
            /// </summary>
            [DataMember]
            public string Language { get; set; }

            /// <summary>
            /// Language
            /// </summary>
            [DataMember]
            public string TimeZone { get; set; }

            /// <summary>
            /// SiteId
            /// </summary>
            [DataMember]
            public int SiteId { get; set; }


        }

}
