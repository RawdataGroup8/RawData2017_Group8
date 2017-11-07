namespace WebServiceLayer.DataTransferObjects
{
    public class UserDTO : BaseDTO
    {
        public new string Url { get; set; }
        public new string Name { get; set; }
        public int NumberOfPosts { get; set; }
    }
}
