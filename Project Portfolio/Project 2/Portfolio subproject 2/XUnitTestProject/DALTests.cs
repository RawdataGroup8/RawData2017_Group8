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
    }
}
