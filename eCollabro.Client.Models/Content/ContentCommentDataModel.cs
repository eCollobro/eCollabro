using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.Client.Models.Content
{
    public class ContentCommentDataModel
    {
        public List<ContentCommentModel> ContentComments { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfVotes { get; set; }
        public double AverageRatings { get; set; }

        public bool UserVoted { get; set; }

        public int UserRating { get; set; }

        public bool UserLiked { get; set; }

        public ContentCommentDataModel()
        {
            ContentComments = new List<ContentCommentModel>();
        }
    }
}
