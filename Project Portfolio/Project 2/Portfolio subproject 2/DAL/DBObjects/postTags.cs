using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DBObjects
{
    public class PostTags
    {
        public int PostId { get; set; }
        public string TagName { get; set; }
        public Post Post { get; set; }
    }
}
