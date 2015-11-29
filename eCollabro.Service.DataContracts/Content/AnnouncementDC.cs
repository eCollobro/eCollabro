// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;

using System.Runtime.Serialization;

#endregion

namespace eCollabro.Service.DataContracts.Content
{
    /// <summary>
    /// Announcement
    /// </summary>
    [DataContract]
    public class AnnouncementDC
    {
        [DataMember]
        public int AnnouncementId { get; set; }

        [DataMember]
        public string AnnouncementTitle { get; set; }

        [DataMember]
        public string AnnouncementDescription { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ExpiryDate { get; set; }

        [DataMember]
        public int SiteId { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool IsAnomynousAccess { get; set; }

        [DataMember]
        public bool IsCommentsAllowed { get; set; }

        [DataMember]
        public bool IsRatingAllowed { get; set; }

        [DataMember]
        public bool IsVotingAllowed { get; set; }

        [DataMember]
        public bool IsLikeAllowed { get; set; }

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
