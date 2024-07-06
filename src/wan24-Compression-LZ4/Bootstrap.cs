using K4os.Compression.LZ4.Streams;
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
        public static void Boot()
        {
            CompressionHelper.Algorithms[Lz4CompressionAlgorithm.ALGORITHM_NAME] = Lz4CompressionAlgorithm.Instance;
            CompressionProfiles.Registered[Lz4CompressionAlgorithm.PROFILE_LZ4_RAW] = new CompressionOptions()
                .IncludeNothing()
                .WithAlgorithm(Lz4CompressionAlgorithm.ALGORITHM_NAME);
            ObjectMapping<LZ4EncoderSettings, LZ4EncoderSettings>.Create()
                .AddAutoMappings()
                .Register();
        }
    }
}
