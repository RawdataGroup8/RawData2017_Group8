using System;

namespace DataAccesLayer.DBObjects
{
    class History //PRIMARY KEY USERID+DATE TIME
    {
        public int Userid { set; get; }
        
        public DateTime DateTime_aded { set; get; }

        public int Linkpost_id { set; get; }

    }
}
