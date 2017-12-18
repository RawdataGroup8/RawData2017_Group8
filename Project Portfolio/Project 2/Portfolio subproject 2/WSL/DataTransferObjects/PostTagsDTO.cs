using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccesLayer.DBObjects;

namespace WebServiceLayer.DataTransferObjects
{
    public class PostTagsDTO : Post
    {
        public string TagName { get; set; }

        public PostTagsDTO(PostTags t)
        {
            TagName = t.TagName;
        }
    }
}
