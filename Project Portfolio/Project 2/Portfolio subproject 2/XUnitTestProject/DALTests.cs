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

        [Fact]
        public void DBProcedure_SearchQuestionByID_ReturnsQuestionList()
        {
            var ds = new DataService();
            var results = ds.SeachQuestionsByTag("YourMum", 10);
            Assert.NotEmpty(results);
        }

    }
}
