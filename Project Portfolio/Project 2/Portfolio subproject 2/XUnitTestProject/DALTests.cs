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
            var user = ds.GetUser_Posts(1);
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
        public void GetPosts_ValidID_ReturnsTags()
        {
            var ds = new DataService();
            var post = ds.GetPosts_Tags(19);
            Assert.Equal(5, post.PostTags.Count);
        }

        /* [Fact]
         public void DBProcedure_SearchQuestionByID_ReturnsQuestionList()
         {
             var ds = new DataService();
            // var results = ds.SeachQuestionsByTag("YourMum", 10);
            // Assert.NotEmpty(results);
         }

         //This test Shows the procedure is returning records, how ever needs further care
         [Fact]
         public void Searching_using_type_Strings()
         {
             var ds = new DataService();
             var text = ds.Searching_usingtype_String();
             Assert.Equal(true, text);

         }
         [Fact]
         public void Retrieve_Answers_Procedure()
         {
             var ds = new DataService();
             var text = ds.Retrieve_Answers_Procedure();
             Assert.Equal(10, text.Count);

         }*/

    }
}
