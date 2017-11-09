using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccesLayer.DBObjects;

namespace WebServiceLayer.DataTransferObjects
{
    public class CommentDTO : BaseDTO
    {
        public int CommentScore { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentCreateDate { get; set; }

        public CommentDTO(Comment pt)
        {
            CommentScore = pt.CommentScore;
            CommentText = pt.CommentText;
            CommentCreateDate = pt.CommentCreateDate;
        }
    }
}
