using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DBObjects
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>();
        }
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public int OwnerUserId { get; set; }
        public int TypeId { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
