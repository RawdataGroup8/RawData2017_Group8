using System;
using System.Collections.Generic;
using System.Linq;
using DataAccesLayer.DBObjects;

namespace WebServiceLayer.DataTransferObjects
{
    public class QuestionDTO : PostDTO
    {
        public int? AcceptedAnswerId { get; set; }
        public DateTime? ClosedDate { get; set; }
        /*public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public int OwnerUserId { get; set; }
        public List<PostTagsDTO> PostTags { get; set; }*/

        public QuestionDTO(Question q, string path)
        {
            Url = path;
            if(q == null) return;

            AcceptedAnswerId = q.AcceptedAnswerId;
            ClosedDate = q.ClosedDate;
            if (q.Post== null) return;

            var p = q.Post;
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
