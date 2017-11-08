using DataAccesLayer;
using Xunit;

namespace XUnitTestProject
{
    public class DataLayerTests
    {
        [Fact]
        public void GetUser_ValidId_ReturnsUsersWithPosts()
        {
            var ds = new DataService();
            var user = ds.GetUser(1);
            Assert.Equal(2, user.Posts.Count);
        }

        [Fact]
        public void GetPost_ValidId_ReturnsFullPost()
        {
            var ds = new DataService();
            var post = ds.GetPost(83073);
            Assert.Equal(post.Score, 665);
            Assert.Equal(5, post.PostTags.Count);
            Assert.Equal(13, post.Comments.Count);
        }

        [Fact]
        public void GetQuestion_ValidId_ReturnsFullQuestion()
        {
            var ds = new DataService();
            var question = ds.GetQuestion(19);
            Assert.True(question.GetPost().Title.Contains("the fastest way to get the value of"));
            Assert.Equal(531, question.AcceptedAnswerId);
        }

        [Fact]
        public void GetAnswer_ValidId_ReturnsFullAnswer()
        {
            var ds = new DataService();
            var question = ds.GetAnswer(71);
            Assert.True(question.GetPost().Body.Contains("general description of a technique"));
            Assert.Equal(19, question.Parentid);
        }

        [Fact]
        public void GetPosts_ValidID_ReturnsPostWithTags() 
        {
            var ds = new DataService();
            var post = ds.GetPosts_Tags(19);
            Assert.Equal(5, post.PostTags.Count);
        }

        [Fact]
        public void FulltextSearch_SearchString_ReturnsSearchResults()
        {
            var ds = new DataService();
            var post = ds.FulltextSearch();
            Assert.Equal(true, post);
        }


        

        [Fact]
        public void InsertMarking_ValidID_InsertsMarking()
        {
            var ds = new DataService();
            var ret = ds.AddMarking(1, 2, "testing");
            Assert.Equal(1, ret);

            //cleanup
            ds.DeleteMarking(1, 2);
        }

        /*
         [Fact]
         public void DBProcedure_SearchQuestionByID_ReturnsQuestionList()
         {
             var ds = new DataService();
            // var results = ds.SeachQuestionsByTag("YourMum", 10);
            // Assert.NotEmpty(results);
         }
         */
        //This test Shows the procedure is returning records, how ever needs further care
        [Fact]
         public void Searching_using_type_Strings()
         {
             var ds = new DataService();
             var text = ds.FulltextSearch();
             Assert.Equal(true, text);

         }
     

        [Fact]
        public void SearchQuestionsByTag()
        {
            var ds = new DataService();
            var text = ds.SearchQuestionsByTag();
            Assert.Equal(5, text.Count);

        }


        

    }
}
