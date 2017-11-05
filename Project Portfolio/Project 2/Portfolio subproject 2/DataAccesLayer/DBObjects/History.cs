using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DBObjects
{
    class History //PRIMARY KEY USERID+DATE TIME
    {
        public int Userid { set; get; }
        
        public DateTime DateTime_aded { set; get; }

        public int Linkpost_id { set; get; }

    }
}
