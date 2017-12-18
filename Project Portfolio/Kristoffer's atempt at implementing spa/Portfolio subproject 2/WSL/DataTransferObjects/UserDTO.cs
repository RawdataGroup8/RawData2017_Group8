using System;
using DataAccesLayer.DBObjects;

namespace WebServiceLayer.DataTransferObjects
{
    public class UserDTO : BaseDTO
    {
        //public new string Url { get; set; }
        public string Name { get; set; }
        public int NumberOfPosts { get; set; }
        public string PostsByUser { get; set; }

        public UserDTO(User user, string url, string postsUrl)
        {
            Url = url;
            if (user == null) return;
            Name = user.UserName;
            NumberOfPosts = user.Posts.Count;
            PostsByUser = postsUrl;
        }
    }
}
