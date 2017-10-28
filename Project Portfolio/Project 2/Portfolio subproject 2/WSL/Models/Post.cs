using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    class Post
    {
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public String Body { get; set; }
        public string Title { get; set; }
        public int OwnerUserId { get; set; }
        public int TypeId { get; set; }

        
        private User PostUser { get; set; }
    }
}
