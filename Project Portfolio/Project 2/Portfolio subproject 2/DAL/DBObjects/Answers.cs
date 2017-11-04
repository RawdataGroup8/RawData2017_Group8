using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace DAL.DBObjects
{
    public class Answers
    {
        [Key]
        public int post_id { get; set; }
        public int Parentid { get; set; }      
    }

    public class Answers1
    {
        [Key]
        public int post_id { get; set; }
        public int Parentid { get; set; }
    }
}
