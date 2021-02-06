using CarShowroomApp;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace CarShowroom.UI.Tests.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            TestClient = appFactory.CreateClient();
        }
    }
}
