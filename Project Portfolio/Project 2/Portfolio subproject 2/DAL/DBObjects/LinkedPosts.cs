using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DBObjects
{
    class LinkedPosts
    {
        public int LinkPostId { get; set; }
        public string PostId { get; set; }
    }
}
