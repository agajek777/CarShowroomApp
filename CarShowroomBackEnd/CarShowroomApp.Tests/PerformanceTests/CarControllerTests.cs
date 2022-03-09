using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarShowroom.UI.Tests.PerformanceTests
{
    [SimpleJob]
    public class CarControllerTests
    {
        private HttpClient _client;

        [Params(10, 100, 1000)]
        public int N;

        [GlobalSetup]
        void Setup()
        {
            _client = new HttpClient();
        }

        [Benchmark]
        public async Task GetAllRequestAsync()
        {
            List<Task> requests = new List<Task>(N);

            for (int i = 0; i < N; i++)
            {
                var getTask = _client.GetAsync("https://localhost:44332/api/Car");
                requests.Add(getTask);
            }

            Task.WaitAll(requests.ToArray());
        }
    }
}
