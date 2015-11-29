// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

#endregion 
namespace eCollabro.Service.DataContracts.Core
{
    /// <summary>
    /// SaveSiteFeaturesRequest
    /// </summary>
    [DataContract]
    public class SaveSiteFeaturesRequest:BaseServiceRequest
    {
        private int siteId;
        private string features;
        private Boolean isCreateNavigationChecked;


        [DataMember]
        public int SiteId { get { return siteId; } set { siteId = value; } }

        [DataMember]
        public string Features { get { return features; } set { features = value; } }

        [DataMember]
        public Boolean IsCreateNavigationChecked { get { return isCreateNavigationChecked; } set { isCreateNavigationChecked = value; } }


 
        public SaveSiteFeaturesRequest()
        {
           
        }
    }
}
