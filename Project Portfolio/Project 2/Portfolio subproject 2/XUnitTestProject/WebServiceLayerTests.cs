using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using System.Text;


namespace XUnitTestProject
{
    public class WebServiceLayerTests
    {
        private const string UsersApi = "http://localhost:5001/api/users";
        private const string QuestionsApi = "http://localhost:5001/api/questions";

        [Fact]
        public void ApiQuestion_GetWithNoArguments_OkAndNewestQuestions()
        {
            var (data, statusCode) = GetArray(UsersApi);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(8, data.Count);
            //Assert.Equal("Beverages", data.First()["name"]);
            //Assert.Equal("Seafood", data.Last()["name"]);
        }

        // Helpers
        private static (JArray, HttpStatusCode) GetArray(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
        }
    }
}
