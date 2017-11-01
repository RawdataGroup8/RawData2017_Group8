using System;
using System.Linq;
using Xunit;
using DAL;
using  DAL.DBObjects;

namespace XUnitTestProject
{
    public class DALTests
    {
        /*[Fact]
        public void Test1()
        {
            var service = new DataService();
            Assert.Equal("Jeff Atwood", service.GetUsers().First().UserName);
        }*/

        [Fact]
        //KI: This test fails if you put ID 2 which is not avaliable in the data base--- needs attention.
        /*RB: it is supposed to - unit tests are specific, this one tests whether the user with id==1 has 2 posts. If you want to test whether
        the expected thing happens if the user has no posts, it needs to be done in a different test. */
        public void GetUser_ValidId_ReturnsUsersWithPosts()
        {
            var ds = new DataService();
            var user = ds.GetUser(1);
            Assert.Equal(2, user.Posts.Count);
        }

        [Fact]
        public void GetPost_ValidId_ReturnsPostWithComments()
        {
            var ds = new DataService();
            var post = ds.GetPost(14088);
            Assert.Equal(post.Score, 176);
            Assert.Equal(24, post.Comments.Count);
        }



    }
}
