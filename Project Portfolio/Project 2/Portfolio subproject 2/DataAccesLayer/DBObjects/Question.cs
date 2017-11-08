using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccesLayer.DBObjects
{
    public class Question
    {
        [Key]
        public int PostId1 { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public DateTime? ClosedDate { get; set; }

        private Post _post;
        public Post GetPost() => _post;

        public void SetPost(Post value) => _post = value;
    }
}
