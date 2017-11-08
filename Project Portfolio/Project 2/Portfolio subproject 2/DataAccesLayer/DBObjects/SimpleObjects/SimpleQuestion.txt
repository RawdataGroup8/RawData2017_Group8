using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.DBObjects.SimpleObjects
{
    public class SimpleQuestion
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Score { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosedDate { get; set; }

    }
}
