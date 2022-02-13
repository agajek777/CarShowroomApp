using BenchmarkDotNet.Running;

namespace CarShowroom.UI.Tests.PerformanceTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<CarControllerTests>();
        }
    }
}
