using eCollabro.BAL.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.CustomModels
{
    public class ContentCommentData
    {
        public List<ContentComment> ContentComments { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfVotes { get; set; }
        public double AverageRatings { get; set; }

        public bool UserVoted { get; set; }

        public int UserRating { get; set; }

        public bool UserLiked { get; set; }

        public ContentCommentData()
        {
            ContentComments = new List<ContentComment>();
        }
    }
}
