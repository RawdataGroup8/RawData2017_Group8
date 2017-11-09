using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccesLayer.DBObjects;

namespace WebServiceLayer.DataTransferObjects
{
    public class AnswerDTO : PostDTO
    {
        public int Parentid { get; set; }
        //public int OwnerUserId { get; set; }
        /*public List<PostTags> PostTags { get; set; }
        public int Score { get; set; }
        public DateTime CreationDate { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }*/

        public AnswerDTO(Answers a, string url)
        {
            
            Url = url;

            if(a == null) return;            
            Parentid = a.Parentid;

            var p = a.GetPost();
            CreationDate = p.CreationDate;
            Score = p.Score;
            Body = p.Body;
            Title = p.Title;
            OwnerUserId = p.OwnerUserId;
            PostTags = p.PostTags.Select(pt => new PostTagsDTO(pt)).ToList();
            Comments = p.Comments.Select(pt => new CommentDTO(pt)).ToList();
        }
    }
}
