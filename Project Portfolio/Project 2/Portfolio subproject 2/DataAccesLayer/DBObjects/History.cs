using System;

namespace DataAccesLayer.DBObjects
{
    public class History //PRIMARY KEY USERID+DATE TIME
    {
        public int Userid { set; get; }
        
        public DateTime DateTimeAdded { set; get; }

        public int LinkPostId { set; get; }

    }
}
