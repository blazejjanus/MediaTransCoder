using System.Text;

namespace MediaTransCoder.Backend {
    /// <summary>
    /// Options used for audio conversion.
    /// </summary>
    public class AudioOptions {
        /// <summary>
        /// Audio codec used in conversion.
        /// </summary>
        public AudioCodecs Codec { get; set; }
        /// <summary>
        /// Audio BitRate
        /// </summary>
        public AudioBitRate BitRate { get; set; }
        /// <summary>
        /// Number of audio channels
        /// </summary>
        public uint AudioChannels {
            get {
                return ac;
            }
            set {
                if(value < 1 || value > 255) {
                    throw new ArgumentOutOfRangeException("AudioChannels");
                }
                ac = value;
            }
        }
        /// <summary>
        /// Audio sampling frequency
        /// </summary>
        public SamplingFrequency SamplingRate { get; set; }
        private uint ac;

        public AudioOptions() {
            Codec = AudioCodecs.mp3;
            BitRate = AudioBitRate.abr192;
            AudioChannels = 2;
            SamplingRate = SamplingFrequency.ar48k;
        }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine("Audio:");
            sb.AppendLine("\tCodec:      " + Codec);
            sb.AppendLine("\tBitRate:    " + BitRate);
            sb.AppendLine("\tSampling:   " + EnumHelper.GetName(SamplingRate));
            sb.AppendLine("\tChannels:   " + AudioChannels);
            return sb.ToString();
        }
    }
}
