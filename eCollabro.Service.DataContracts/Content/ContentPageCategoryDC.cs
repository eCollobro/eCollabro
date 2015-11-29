// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts.Content
{
    /// <summary>
    /// ContentPageCategoryDC
    /// </summary>
    [DataContract]
    public class ContentPageCategoryDC
    {
        [DataMember]
        public int ContentPageCategoryId { get; set; }

        [DataMember]
        public string ContentPageCategoryName { get; set; }

        [DataMember]
        public string ContentPageCategoryDescription { get; set; }

        [DataMember]
        public int SiteId { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool IsAnomynousAccess { get; set; }

        [DataMember]
        public int CreatedById { get; set; }

        [DataMember]
        public System.DateTime CreatedOn { get; set; }

        [DataMember]
        public Nullable<int> ModifiedById { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        [DataMember]
        public List<ContentPageDC> ContentPages { get; set; }

        [DataMember]
        public int NumberOfContentPages { get; set; }

    }
}
