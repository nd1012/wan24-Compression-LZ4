using wan24.Compression.LZ4;
using wan24.Compression.Tests;

namespace wan24_Compression_Tests
{
    [TestClass]
    public class Algorithm_Tests
    {
        static Algorithm_Tests() => Bootstrap.Boot();

        [TestMethod]
        public void Sync_Tests()
        {
            AlgorithmTests.TestAllAlgorithms();
        }

        [TestMethod]
        public async Task Async_Tests()
        {
            await AlgorithmTests.TestAllAlgorithmsAsync();
        }
    }
}
