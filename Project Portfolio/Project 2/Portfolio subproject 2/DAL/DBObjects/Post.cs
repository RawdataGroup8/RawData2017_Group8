using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace DAL.DBObjects
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>();
        }
        public int PostId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public int OwnerUserId { get; set; }
        public int TypeId { get; set; }

        public User User { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class Post1
    {
       
        public int post_id { get; set; }
        //public DateTime? CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
       
       
    }
}
