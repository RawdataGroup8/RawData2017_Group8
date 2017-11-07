using System;
using DataAccesLayer.DBObjects;

namespace WebServiceLayer.DataTransferObjects
{
    public class QuestionDTO : BaseDTO
    {
        public int AcceptedAnswerId { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public int OwnerUserId { get; set; }
        public QuestionDTO(Question q, string path)
        {
            Url = path;
            var p = q.GetPost();
            AcceptedAnswerId = q.AcceptedAnswerId;
            ClosedDate = q.ClosedDate;
            CreationDate = p.CreationDate;
            Score = p.Score;
            Body = p.Body;
            Title = p.Title;
            OwnerUserId = p.OwnerUserId;
        }
    }
}
