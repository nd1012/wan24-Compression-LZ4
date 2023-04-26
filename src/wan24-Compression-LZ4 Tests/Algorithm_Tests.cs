using wan24.Compression;
using wan24.Compression.LZ4;

namespace wan24_Compression_Tests
{
    [TestClass]
    public class Algorithm_Tests
    {
        public readonly int[] Algorithms = new int[]
        {
            GZipCompressionAlgorithm.ALGORITHM_VALUE,
            BrotliCompressionAlgorithm.ALGORITHM_VALUE
        };

        static Algorithm_Tests() => Bootstrap.Boot();

        [TestMethod]
        public void Sync_Tests()
        {
            Sync_Tests(Lz4CompressionAlgorithm.ALGORITHM_VALUE);
        }

        [TestMethod]
        public async Task Async_Tests()
        {
            await Async_Tests(Lz4CompressionAlgorithm.ALGORITHM_VALUE);
        }

        public static readonly byte[] Data = new byte[81920];

        public static void Sync_Tests(int algo)
        {
            string name = CompressionHelper.GetAlgorithm(algo).Name;
            // Default
            {
                CompressionOptions options = CompressionHelper.GetDefaultOptions();
                options.Algorithm = name;
                options.LeaveOpen = true;
                using MemoryStream data = new(Data);
                using MemoryStream compressed = new();
                data.Compress(compressed, options);
                Assert.IsTrue(compressed.Length < data.Length);
                compressed.Position = 0;
                using MemoryStream uncompressed = new();
                compressed.Decompress(uncompressed, options);
                Assert.IsTrue(Data.SequenceEqual(uncompressed.ToArray()));
            }
            // Nothing included
            {
                CompressionOptions options = CompressionHelper.GetDefaultOptions();
                options.Algorithm = name;
                options.Flags = CompressionFlags.None;
                options.LeaveOpen = true;
                using MemoryStream data = new(Data);
                using MemoryStream compressed = new();
                data.Compress(compressed, options);
                Assert.IsTrue(compressed.Length < data.Length);
                compressed.Position = 0;
                using MemoryStream uncompressed = new();
                compressed.Decompress(uncompressed, options);
                Assert.IsTrue(Data.SequenceEqual(uncompressed.ToArray()));
            }
        }

        public static async Task Async_Tests(int algo)
        {
            string name = CompressionHelper.GetAlgorithm(algo).Name;
            // Default
            {
                CompressionOptions options = CompressionHelper.GetDefaultOptions();
                options.Algorithm = name;
                options.LeaveOpen = true;
                using MemoryStream data = new(Data);
                using MemoryStream compressed = new();
                await data.CompressAsync(compressed, options);
                Assert.IsTrue(compressed.Length < data.Length);
                compressed.Position = 0;
                using MemoryStream uncompressed = new();
                await compressed.DecompressAsync(uncompressed, options);
                Assert.IsTrue(Data.SequenceEqual(uncompressed.ToArray()));
            }
            // Nothing included
            {
                CompressionOptions options = CompressionHelper.GetDefaultOptions();
                options.Algorithm = name;
                options.Flags = CompressionFlags.None;
                options.LeaveOpen = true;
                using MemoryStream data = new(Data);
                using MemoryStream compressed = new();
                await data.CompressAsync(compressed, options);
                Assert.IsTrue(compressed.Length < data.Length);
                compressed.Position = 0;
                using MemoryStream uncompressed = new();
                await compressed.DecompressAsync(uncompressed, options);
                Assert.IsTrue(Data.SequenceEqual(uncompressed.ToArray()));
            }
        }
    }
}
