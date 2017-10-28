using System;

namespace WSL.Models
{
    class Question
    {
        public int AcceptedAnswerId { get; set; }
        public DateTime ClosedDate { get; set; }

        public Post QuestionPost { get; set; }
       

        

    }
}
