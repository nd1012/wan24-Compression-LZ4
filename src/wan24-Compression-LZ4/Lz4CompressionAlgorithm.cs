using K4os.Compression.LZ4;
using K4os.Compression.LZ4.Streams;
using System.IO.Compression;
using wan24.Core;
using wan24.MappingObject;

namespace wan24.Compression.LZ4
{
    /// <summary>
    /// LZ4 compression algorithm
    /// </summary>
    public sealed record class Lz4CompressionAlgorithm : CompressionAlgorithmBase
    {
        /// <summary>
        /// Algorithm name
        /// </summary>
        public const string ALGORITHM_NAME = "LZ4";
        /// <summary>
        /// Algorithm value
        /// </summary>
        public const int ALGORITHM_VALUE = 2;
        /// <summary>
        /// LZ4 raw (without header) profile key
        /// </summary>
        public const string PROFILE_LZ4_RAW = "LZ4_RAW";

        /// <summary>
        /// Static constructor
        /// </summary>
        static Lz4CompressionAlgorithm() => Instance = new();

        /// <summary>
        /// Constructor
        /// </summary>
        private Lz4CompressionAlgorithm() : base(ALGORITHM_NAME, ALGORITHM_VALUE) { }

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static Lz4CompressionAlgorithm Instance { get; }

        /// <summary>
        /// Encoder settings
        /// </summary>
        public static LZ4EncoderSettings EncoderSettings { get; set; } = new();

        /// <summary>
        /// Decoder settings
        /// </summary>
        public static LZ4DecoderSettings DecoderSettings { get; set; } = new();

        /// <inheritdoc/>
        protected override Stream CreateCompressionStream(Stream compressedTarget, CompressionOptions options)
        {
            if (options.Level == CompressionLevel.NoCompression) return new LimitedStream(compressedTarget, canRead: false, canWrite: true, canSeek: false, options.LeaveOpen);
            LZ4EncoderSettings settings = Mappings.MapTo(EncoderSettings, new LZ4EncoderSettings());
            settings.CompressionLevel = options.Level switch
            {
                CompressionLevel.Fastest => LZ4Level.L00_FAST,
                CompressionLevel.Optimal => LZ4Level.L11_OPT,
                CompressionLevel.SmallestSize => LZ4Level.L12_MAX,
                _ => throw new ArgumentException("Invalid compression level", nameof(options))
            };
            return LZ4Stream.Encode(compressedTarget, settings, options.LeaveOpen);
        }

        /// <inheritdoc/>
        protected override Stream CreateDecompressionStream(Stream source, CompressionOptions options) => LZ4Stream.Decode(source, DecoderSettings, options.LeaveOpen);
    }
}
