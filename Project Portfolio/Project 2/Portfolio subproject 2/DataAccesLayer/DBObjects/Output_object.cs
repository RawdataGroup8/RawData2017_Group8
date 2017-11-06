using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.DBObjects
{
    class Output_object
    {
        public int post_id { get; set; }
        public int question_id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public float score { get; set; }
    }
}
