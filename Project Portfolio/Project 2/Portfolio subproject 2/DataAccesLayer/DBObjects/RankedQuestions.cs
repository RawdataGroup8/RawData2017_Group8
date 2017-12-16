using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccesLayer.DBObjects
{
    public class RankedQuestions
    {
        [Key]
        public int Id { get; set; }
        public double Rank { get; set; }
        public string Title { get; set; }

        public Question Question;
        /*public Question Question => _question;
        public void SetQuestion(Question value) => _question = value;*/
    }
}
