using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccesLayer.DBObjects;

namespace DAL.DBObjects
{
    public class LinkedPosts
    {
        public int LinkPostId { get; set; }
        public int PostId { get; set; }

        //public List<Post> Post { get; set; }
    }
}
