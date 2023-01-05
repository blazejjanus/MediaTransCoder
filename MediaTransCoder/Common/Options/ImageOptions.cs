using System.Numerics;
using System.Text;

namespace MediaTransCoder.Backend {
    public class ImageOptions {
        public ImageFormat Format {
            get {
                return format;
            }
            set {
                format = value;
                if(format == ImageFormat.JPG) { //Set and reset compression level
                    cl = 5;
                } else {
                    cl = null;
                }
            }
        }
        /// <summary>
        /// JPEG compression level 1 - 31.
        /// </summary>
        public int? CompressionLevel {
            get {
                return cl;
            }
            set {
                if(value < 1 || value > 310) {
                    throw new ArgumentOutOfRangeException();
                }
                cl = value;
            }
        }
        public Vector2 Size { get; set; }
        public PixelFormats PixelFormat { get; set; }
        public ImageEffects? Effect { get; set; } = null;
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
        private string? FormattedBrightness {
            get {
                if (brightness.HasValue) {
                    return "brightness=" + ((double)brightness / 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
                }
                return null;
            }
        }
        private string? FormattedContrast {
            get {
                if (contrast.HasValue) {
                    return "contrast=" + ((double)contrast / 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
                }
                return null;
            }
        }
        private string? FormattedSaturation {
            get {
                if (saturation.HasValue) {
                    return "saturation=" + ((double)saturation / 100).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
                }
                return null;
            }
        }
        private ImageFormat format;
        private int? cl = null;
        private int? brightness = null;
        private int? contrast = null;
        private int? saturation = null;

        public string GetVF() {
            var eq = GetEq();
            string result = string.Empty;
            if(eq == null) {
                result = " -vf scale=" + Size.X + ":" + Size.Y;
            } else {
                result = " -vf \"scale=" + Size.X + ":" + Size.Y + "," + eq + "\"";
            }
            return result;
        }

        private string? GetEq() {
            if (brightness != null || contrast != null || saturation != null) {
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
                if(Effect != null) {
                    sb.Append("," + EnumHelper.GetCommand(Effect.Value));
                }
                return sb.ToString();
            } else {
                return null;
            }
        }

        public ImageOptions() {
            Format = ImageFormat.JPG;
            CompressionLevel = 5;
            Size = new Vector2(1920, 1080);
            PixelFormat = PixelFormats.RGBA;
        }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine("Image:");
            sb.AppendLine("\tFormat:             " + Format);
            sb.AppendLine("\tResolution:         " + Size.X + "x" + Size.Y);
            sb.AppendLine("\tPixelFormat:        " + PixelFormat);
            if (cl != null) {
                sb.AppendLine("\tCompressionLevel:   " + CompressionLevel);
            }
            if (brightness != null) {
                sb.AppendLine("\tBrightness:         " + Brightness + "%");
            }
            if (contrast != null) {
                sb.AppendLine("\tContrast:           " + Contrast + "%");
            }
            if (saturation != null) {
                sb.AppendLine("\tSaturation:         " + Saturation + "%");
            }
            if (Effect != null) {
                sb.AppendLine("\tVisualEffect:       " + EnumHelper.GetName(Effect.Value));
            }
            return sb.ToString();
        }
    }
}
