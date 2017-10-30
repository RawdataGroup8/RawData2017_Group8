using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace DAL.DBObjects
{
    class Answers
    {

        public int Parentid { get; set; }
        [Key]
        public int Postid1 { get; set; }

    }
}
