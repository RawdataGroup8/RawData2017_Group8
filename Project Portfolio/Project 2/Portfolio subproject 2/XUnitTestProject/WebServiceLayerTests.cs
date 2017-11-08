using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;


namespace XUnitTestProject
{
    public class WebServiceLayerTests
    {
        private const string UsersApi = "http://localhost:49457/api/user";
        private const string PostsApi = "http://localhost:49457/api/post";
        private const string QuestionsApi = "http://localhost:49457/api/questions";

        [Fact]
        public void ApiQuestion_GetWithNoArguments_OkAndNewestQuestions()
        {
            var (data, statusCode) = GetArray(QuestionsApi);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(10, data.Count);
            Assert.Equal(3344591, data.First()["ownerUserId"]);
            Assert.Equal(158026, data.Last()["ownerUserId"]);
            Assert.Equal("Display CP437 characters on Linux console from Python", data.Last()["title"]);
            Assert.True((DateTime)data.First()["creationDate"] > (DateTime)data.Last()["creationDate"]);
        }

        [Fact]
        public void ApiQuestion_ValidId_OkAndQuestion()
        {
            var (question, statusCode) = GetObject($"{QuestionsApi}/28905111");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(question["score"],0);
            Assert.Equal("How to update the ng-repeat model of controller from another controller?", question["title"]); 
        }

        [Fact]
        public void ApiQuestion_InvalidId_NotFound()
        {
            var (question, statusCode) = GetObject($"{QuestionsApi}/0");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public void ApiPost_ValidId_OkAndPost()
        {
            Assert.True(false);
        }

        [Fact]
        public void ApiUser_ValidId_OkAndUser()
        {
            var (user, statusCode) = GetObject($"{UsersApi}/1");
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("Jeff Atwood", user["name"]);
            Assert.Equal(2, user["numberOfPosts"]);
        }

        [Fact]
        public void ApiUser_UserPosts_OkAndListOfPosts()
        {
            var (posts, statusCode) = GetArray($"{PostsApi}/1/posts");
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(2, posts["number_Of_Posts"]);
        }

        [Fact]
        public void ApiUser_InbalidId_NotFound()
        {
            var (user, statusCode) = GetObject($"{UsersApi}/0");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public void ApiUser_GetWithNoArguments_OkAndListOfUsers()
        {
            var (user, statusCode) = GetArray(UsersApi);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(10, user.Count);
            //Assert.Equal("Jeff Atwood", user.First["name"]);
            //Assert.Equal("denny", user.Last["name"]);
        }



        // Helpers
        private static (JArray, HttpStatusCode) GetArray(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        private static (JObject, HttpStatusCode) GetObject(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }
    }
}
