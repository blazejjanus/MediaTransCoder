using System.Numerics;
using System.Text;

namespace MediaTransCoder.Backend {
    public class ImageOptions {
        public ImageFormat Format { get; set; }
        /// <summary>
        /// JPEG compression level 1 - 31.
        /// </summary>
        public int? CompressionLevel {
            get {
                return cl;
            }
            set {
                if(value < 1 || value > 31) {
                    throw new ArgumentOutOfRangeException();
                }
                cl = value;
            }
        }
        public Vector2 Size { get; set; }
        public PixelFormats PixelFormat { get; set; }
        public ImageEffects? Effect { get; set; }
        public int? Brightness {
            get {
                return brightness;
            }
            set {
                if(value < -100 || value > 100) {
                    throw new ArgumentOutOfRangeException();
                }
                brightness = value;
            }
        }
        public int? Contrast {
            get {
                return contrast;
            }
            set {
                if (value < -100 || value > 100) {
                    throw new ArgumentOutOfRangeException();
                }
                contrast = value;
            }
        }
        public int? Saturation {
            get {
                return contrast;
            }
            set {
                if (value < -100 || value > 100) {
                    throw new ArgumentOutOfRangeException();
                }
                contrast = value;
            }
        }
        public string? FormattedBrightness {
            get {
                if (brightness.HasValue) {
                    return "brightness=" + ((double)brightness / 100).ToString();
                }
                return null;
            }
        }
        public string? FormattedContrast {
            get {
                if (contrast.HasValue) {
                    return "contrast=" + ((double)contrast / 100).ToString();
                }
                return null;
            }
        }
        public string? FormattedSaturation {
            get {
                if (saturation.HasValue) {
                    return "saturation=" + ((double)saturation / 100);
                }
                return null;
            }
        }
        public string? FfmpegEq {
            get {
                if(brightness != null || contrast != null || saturation != null) {
                    var sb = new StringBuilder();
                    sb.Append(" eq=");
                    string? param = null;
                    param = FormattedBrightness;
                    if (param != null) {
                        sb.Append(param + ":");
                    }
                    param = FormattedContrast;
                    if (param != null) {
                        sb.Append(param + ":");
                    }
                    param = FormattedSaturation;
                    if (param != null) {
                        sb.Append(param);
                    }
                    return sb.ToString();
                } else {
                    return null;
                }
            }
        }
        private int? cl = null;
        private int? brightness = null;
        private int? contrast = null;
        private int? saturation = null;

        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine("Audio:");
            sb.AppendLine("\t:");
            return sb.ToString();
        }
    }
}
