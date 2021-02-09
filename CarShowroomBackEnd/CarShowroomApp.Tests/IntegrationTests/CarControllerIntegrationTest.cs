using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Parameters;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CarShowroom.UI.Tests.IntegrationTests
{
    public class CarControllerIntegrationTest : IntegrationTest
    {
        [Fact]
        public async Task GetAllAsync_TwoCarsInDb_OkObjectResultWithList()
        {
            await AuthenticateAsync();

            var response = await TestClient.GetAsync("api/car/");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<PagedList<CarDto>>()).Should().HaveCount(2);
        }

        [Fact]
        public async Task Get_TwoCarsInDbValidId_OkObjectResultWithModel()
        {
            await AuthenticateAsync();

            var response = await TestClient.GetAsync("api/car/1");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<CarDto>()).Should().BeOfType(typeof(CarDto));
        }
    }
}
