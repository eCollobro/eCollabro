// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace eCollabro.Client.Models.Content
{
    /// <summary>
    /// ImageGallery
    /// </summary>
    public class ImageGalleryModel
    {


        public int ImageGalleryId { get; set; }

        [DisplayName("Image Gallery"),Required]
        public string ImageGalleryName { get; set; }

        [DisplayName("Description")]
        public string ImageGalleryDescription { get; set; }


        public int SiteId { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        [DisplayName("Anomynous Access")]
        public bool IsAnomynousAccess { get; set; }



        public int CreatedById { get; set; }


        public System.DateTime CreatedOn { get; set; }


        public Nullable<int> ModifiedById { get; set; }


        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public List<ImageModel> Images {get;set;}

        [DisplayName("Number of Images")]
        public int NumberOfImages { get; set; }

    }
}
