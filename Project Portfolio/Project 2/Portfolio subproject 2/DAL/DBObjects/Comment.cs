using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DBObjects
{
    class Comment
    {
        public int CommentId { get; set; }
        public int CommentScore { get; set; }
        public int CommentText { get; set; }
        public int CommentCreatedate { get; set; }
        //public User CommentUser { get; set; }
    }
}
