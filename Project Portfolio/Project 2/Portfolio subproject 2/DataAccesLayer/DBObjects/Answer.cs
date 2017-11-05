using DAL.DBObjects;

namespace DataAccesLayer.DBObjects
{
    public class Answers
    {
        public int PostId { get; set; }
        public int Parentid { get; set; }

        private Post _post;
        public Post GetPost() => _post;
        public void SetPost(Post value) => _post = value;
    }
}
