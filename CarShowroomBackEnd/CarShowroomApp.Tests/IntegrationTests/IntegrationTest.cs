using CarShowroom.Domain.Models.Authentication;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Infra.Data.Context;
using CarShowroomApp;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CarShowroom.UI.Tests.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DatabaseContext<User, Role>));
                        services.AddDbContext<DatabaseContext<User, Role>>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });

            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("api/auth/register", new UserForRegisterDto()
            {
                UserName = "Test",
                Password = "#Mastercard1"
            });

            return (await response.Content.ReadAsAsync<AuthSuccessResponse>()).Token.ToString();
        }
    }
}
