// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;
using System.ComponentModel;

#endregion 

namespace eCollabro.Client.Models.Content
{
    /// <summary>
    /// BlogCategoryModel
    /// </summary>
    public class BlogCategoryModel
    {

        public int BlogCategoryId { get; set; }

        [DisplayName("Blog Category")]
        public string BlogCategoryName { get; set; }


        [DisplayName("Description")]
        public string BlogCategoryDescription { get; set; }


        public int SiteId { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        [DisplayName("Allow Anomynous Access")]
        public bool IsAnomynousAccess { get; set; }


        public int CreatedById { get; set; }


        public System.DateTime CreatedOn { get; set; }


        public Nullable<int> ModifiedById { get; set; }


        public Nullable<System.DateTime> ModifiedOn { get; set; }


        public List<BlogModel> Blogs { get; set; }

        public int NumberOfBlogs { get; set; }

    }
}
