using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DBObjects
{
    public class User
    {
        public User()
        {
            Posts = new List<Post>();

        }
        public int Userid { get; set; }
        public string UserName { get; set; }
        public DateTime UserCreationDate { get; set; }
        public string UserLocation { get; set; }
        public int? Userage { get; set; }

        public List<Post> Posts { get; set; }
    }
}
