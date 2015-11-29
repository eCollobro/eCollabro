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
    /// ImageGalleryDC
    /// </summary>
    [DataContract]
    public class ImageGalleryDC
    {
        [DataMember]
        public int ImageGalleryId { get; set; }

        [DataMember]
        public string ImageGalleryName { get; set; }

        [DataMember]
        public string ImageGalleryDescription { get; set; }

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
        public List<ImageDC> Images { get;set;}

        [DataMember]
        public int NumberOfImages { get; set; }
    }
}
