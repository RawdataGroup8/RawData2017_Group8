using DataAccesLayer.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceLayer.DataTransferObjects
{
    public class HistoryDTO : BaseDTO
    {
        public int Userid { get; }
        public DateTime LinkPostId { get; }
        public int DateTimeAdded { get; }
        public HistoryDTO(History h)
        {

            if (h == null) return;
            Userid = h.Userid;
            LinkPostId = h.DateTimeAdded;
            DateTimeAdded = h.LinkPostId;
        }

       
    }
}
