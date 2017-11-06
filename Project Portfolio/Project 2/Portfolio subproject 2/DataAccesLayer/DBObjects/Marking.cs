using System;

namespace DataAccesLayer.DBObjects
{
    public class Marking
    {
        public int Userid { get; set; }
        public int Postid { get; set; }
        public DateTime Datetime_added { get; set; }
        public string Folder_label { get; set; }
    }
}
