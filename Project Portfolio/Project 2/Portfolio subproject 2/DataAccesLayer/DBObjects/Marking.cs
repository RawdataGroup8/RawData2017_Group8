using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DBObjects
{
    class Marking
    {
        public int Userid { get; set; }
        public int Postid { get; set; }
        public DateTime Datetime_added { get; set; }
        public string Folder_label { get; set; }
    }
}
