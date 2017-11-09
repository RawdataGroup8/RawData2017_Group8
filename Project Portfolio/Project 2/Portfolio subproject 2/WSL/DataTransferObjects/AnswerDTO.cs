using DataAccesLayer.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceLayer.DataTransferObjects
{
    public class AnswerDTO : BaseDTO
    {

        public int PostId { get; set; }
        public int Parentid { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public int OwnerUserId { get; set; }
        //public List<PostTagsDTO> PostTags { get; set; }

        public AnswerDTO(Answers a, string path)
        {
           /* Url = path;
            if (a == null&&a.TypeId==2) return;

            PostId = a.PostId;
            Score = a.Score;
            Body = a.Body;
            Title = a.Title;
            OwnerUserId = a.OwnerUserId;
            //var tags = p.PostTags.Select(pt => new PostTagsDTO(pt)).ToList();
            //PostTags = tags;*/
        }
    }

}
