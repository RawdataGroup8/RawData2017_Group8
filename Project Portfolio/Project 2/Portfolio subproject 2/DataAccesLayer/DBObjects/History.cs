using System;

namespace DataAccesLayer.DBObjects
{
    public class History
    {
        public int Userid { set; get; }
        
        public DateTime DateTimeAdded { set; get; }

        public int LinkPostId { set; get; }

        public History(){} //empty ctor for needed by EF
        public History(int userId, int linkPostId)
        {
            Userid = userId;
            LinkPostId = linkPostId;
            DateTimeAdded = DateTime.Now;
        }

    }
}
