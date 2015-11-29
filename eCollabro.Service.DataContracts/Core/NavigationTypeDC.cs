// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System;
using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts.Core
{
    /// <summary>
    /// NavigationTypeDC
    /// </summary>
    [DataContract]
    public class NavigationTypeDC
    {
        [DataMember]
        public int NavigationTypeId { get; set; }

        [DataMember]
        public string NavigationTypeCode { get; set; }

        [DataMember]
        public string NavigationType { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int CreatedById { get; set; }

        [DataMember]
        public System.DateTime CreatedOn { get; set; }

        [DataMember]
        public Nullable<int> ModifiedById { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
