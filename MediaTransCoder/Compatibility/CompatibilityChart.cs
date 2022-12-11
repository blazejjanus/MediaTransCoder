using System.Text;

namespace MediaTransCoder.Backend.Compatibility {
    public class CompatibilityChart {
        /// <summary>
        /// Represents container type (format)
        /// </summary>
        public ContainerFormat Foramt { get; set; }
        /// <summary>
        /// List or audio codecs compatible with container format if requested, otherwise null
        /// </summary>
        public List<AudioCodecs>? AudioCodecs { get; set; }
        /// <summary>
        /// List or video codecs compatible with container format if requested, otherwise null
        /// </summary>
        public List<VideoCodecs>? VideoCodecs { get; set; }
        public string ToString(bool inline = false) {
            var sb  = new StringBuilder();
            string separator = "\n";
            if (inline)
                separator = " ";
            sb.Append(EnumHelper.GetName(Foramt) + ":" + separator);
            if (AudioCodecs != null) {
                sb.Append("Audio:" + separator);
                for(int i = 0; i < AudioCodecs.Count; i++) {
                    sb.Append(EnumHelper.GetName(AudioCodecs.ElementAt(i)));
                    if(i < AudioCodecs.Count- 1) {
                        sb.Append(", ");
                    }
                }
                sb.Append(separator);
            }
            if (VideoCodecs != null) {
                sb.Append("Video:" + separator);
                for (int i = 0; i < VideoCodecs.Count; i++) {
                    sb.Append(EnumHelper.GetName(VideoCodecs.ElementAt(i)));
                    if (i < VideoCodecs.Count - 1) {
                        sb.Append(", ");
                    }
                }
                sb.Append(separator);
            }
            return sb.ToString();
        }
        public CompatibilityChart(ContainerFormat format) { 
            Foramt = format;
        }
    }
}
