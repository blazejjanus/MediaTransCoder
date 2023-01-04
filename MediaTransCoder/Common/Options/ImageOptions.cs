using System.Numerics;
using System.Text;

namespace MediaTransCoder.Backend {
    public class ImageOptions {
        public ImageFormat Format { get; set; }
        /// <summary>
        /// JPEG compression level 1 - 31.
        /// </summary>
        public int? CompressionLevel { get; set; }
        public Vector2 Size { get; set; }
        public PixelFormats PixelFormat { get; set; }
        public int? Brightness { get; set; }
        public int? Contrast { get; set; }
        public int? Saturation { get; set; }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine("Audio:");
            sb.AppendLine("\t:");
            return sb.ToString();
        }
    }
}
