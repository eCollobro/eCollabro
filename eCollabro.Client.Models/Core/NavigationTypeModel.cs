// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System;

#endregion 

namespace eCollabro.Client.Models.Core
{
    public class NavigationTypeModel
    {
        public int NavigationTypeId { get; set; }

        public string NavigationTypeCode { get; set; }

        public string NavigationType { get; set; }

        public bool IsActive { get; set; }

        public int CreatedById { get; set; }

        public System.DateTime CreatedOn { get; set; }

        public Nullable<int> ModifiedById { get; set; }

        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
