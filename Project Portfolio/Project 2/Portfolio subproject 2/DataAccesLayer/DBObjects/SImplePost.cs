using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccesLayer.DBObjects
{
    public class SimplePost
    {
        [Key]
        public int PostId { get; set; }
        public int Score { get; set; }
        public string Title { get; set; }
        public int TypeId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
