using System;
using System.Linq;
using Xunit;
using DAL;
using  DAL.DBObjects;

namespace XUnitTestProject
{
    public class DALTests
    {
        [Fact]
        public void Test1()
        {
            var service = new DataService();
            Assert.Equal("Jeff Atwood", service.GetUsers().First().UserName);
        }
        [Fact]
        public void Test_PostWithComments()
        {
            var ds = new DataService();
            var post = ds.GetPost(14088);
            Assert.Equal(post.Score, 176);
            Assert.Equal(24, post.Comments.Count);
        }



    }
}
