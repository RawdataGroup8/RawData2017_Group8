using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccesLayer.DBObjects;

namespace WebServiceLayer.DataTransferObjects
{
    public class PostDTO : BaseDTO
    {
        public new string Url { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string AuthorUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public List<PostTagsDTO> PostTags { get; set; }
        public int OwnerUserId { get; set; }

    }
}
