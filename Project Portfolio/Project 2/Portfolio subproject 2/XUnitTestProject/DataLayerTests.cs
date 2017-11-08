using DataAccesLayer;
using DataAccesLayer.DBObjects;
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
        
//        [Fact]
//        public void History_AddAndRemoveQuestionsFromHistory()
//        {
//            var ds = new DataService();
//            ds.AddQuestionToHistory(19, 13);
//            Assert.NotEmpty(ds.GetHistory());
//            Assert.True(ds.GetHistory().Count == 1);
//            
//            ds.RemoveQuestionFromHistory(19);
//            Assert.Empty(ds.GetHistory());
//        }
        

        [Fact]
        public void GetLinkedPosts_returnsPostList()
        {
            DataService ds = new DataService();
            Assert.NotEmpty(ds.LinkedFromThisPost(19));
        }

        public void GetLinkedPosts_reverse_returnsPostList()
        {
            DataService ds = new DataService();
            Assert.NotEmpty(ds.LinkingToThisPost(19));
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
        

        [Fact]
         public void FulltextSearch_StringSentence_ReturnsRelevantPosts()
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
