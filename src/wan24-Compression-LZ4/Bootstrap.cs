using wan24.Core;

[assembly: Bootstrapper(typeof(wan24.Compression.LZ4.Bootstrap), nameof(wan24.Compression.LZ4.Bootstrap.Boot))]

namespace wan24.Compression.LZ4
{
    /// <summary>
    /// Bootstrapper
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// Boot
        /// </summary>
        public static void Boot() => CompressionHelper.Algorithms[Lz4CompressionAlgorithm.ALGORITHM_NAME] = Lz4CompressionAlgorithm.Instance;
    }
}
