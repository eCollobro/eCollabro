using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.Service.DataContracts.Content
{
    /// <summary>
    /// ContentCommentDataDC
    /// </summary>
    [DataContract]
    public class ContentCommentDataDC
    {
        [DataMember]
        public List<ContentCommentDC> ContentComments { get; set; }

        [DataMember]
        public int NumberOfLikes { get; set; }

        [DataMember]
        public int NumberOfVotes { get; set; }


        [DataMember]
        public double AverageRatings { get; set; }

        [DataMember]
        public int UserRating { get; set; }


        [DataMember]
        public bool UserLiked { get; set; }

        public ContentCommentDataDC()
        {
            ContentComments = new List<ContentCommentDC>();
        }
    }
}
