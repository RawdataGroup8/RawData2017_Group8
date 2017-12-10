namespace DataAccesLayer.DBObjects
{
    public class PostTags
    {
        public int PostId { get; set; }
        public string TagName { get; set; }
        public Post Post { get; set; }
    }
}
