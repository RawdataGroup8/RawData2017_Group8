using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccesLayer.DBObjects
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>();
            PostTags = new List<PostTags>();
        }
        [Key]
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public int OwnerUserId { get; set; }
        public int TypeId { get; set; }

        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        public List<PostTags> PostTags { get; set; }
    }

    
    
}
