using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceLayer.DataTransferObjects
{
    public class PostDTO : BaseDTO
    {
        public new string Url { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public string Comments { get; set; }
        public string PostTags { get; set; }
    }
}
