namespace Exercise4_1.DomainModel
{
    public class Post
    {
        public int Id { get; set; }
        public int PostType { get; set; }
        public int? ParentId { get; set; }
        public string CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
    }
}
