using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace DAL.DBObjects
{
    class Question
    {
        [Key]
        public int Postid { get; set; }
        public int AcceptedAnswerId { get; set; }
        public DateTime ClosedDate { get; set; }
    }
}
