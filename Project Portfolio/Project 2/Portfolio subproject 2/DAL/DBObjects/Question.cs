using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DBObjects
{
    class Question
    {
        public int AcceptedAnswerId { get; set; }
        public DateTime ClosedDate { get; set; }

        public Post QuestionPost { get; set; }
    }
}
