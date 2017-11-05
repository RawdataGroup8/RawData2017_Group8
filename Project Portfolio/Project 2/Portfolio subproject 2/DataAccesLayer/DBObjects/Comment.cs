using System;

namespace DataAccesLayer.DBObjects
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int CommentScore { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentCreateDate { get; set; }
        public int PostId { get; set; }
        //public User CommentUser { get; set; }
        public Post Post { get; set; }


    }
}
