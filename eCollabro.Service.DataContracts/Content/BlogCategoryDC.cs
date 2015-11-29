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
    /// BlogCategoryDC
    /// </summary>
    [DataContract]
    public class BlogCategoryDC
    {
        [DataMember]
        public int BlogCategoryId { get; set; }

        [DataMember]
        public string BlogCategoryName { get; set; }

        [DataMember]
        public string BlogCategoryDescription { get; set; }

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
        public List<BlogDC> Blogs { get; set; }

        [DataMember]
        public int NumberOfBlogs { get; set; }

    }
}
