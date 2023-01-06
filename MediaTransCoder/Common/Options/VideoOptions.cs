using System.Text;

namespace MediaTransCoder.Backend {
    public class VideoOptions {
        //TODO: Get propper values by codec
        /// <summary>
        /// Video codec
        /// </summary>
        public VideoCodecs Codec { get; set; }
        /// <summary>
        /// Video resolution
        /// </summary>
        public Resolutions Resolution { get; set; }
        /// <summary>
        /// Video bitrate
        /// </summary>
        public int BitRate {
            get {
                return br;
            }
            set {
                if(value < 400 || value > 150000) {
                    throw new ArgumentOutOfRangeException("FPS");
                }
                //TODO: Check if bitrate is compatible with resolution
                br = value;
            }
        }

        /// <summary>
        /// Video frames per second
        /// </summary>
        public int FPS {
            get {
                return fps;
            }
            set { 
                if(value < 20 || value > 120) {
                    throw new ArgumentOutOfRangeException("FPS");
                }
                fps = value;
            } 
        }
        
        /// <summary>
        /// If true audio will be removed
        /// </summary>
        public bool RemoveAudio { get; set; }

        private int fps;
        private int br;

        public VideoOptions() {
            Codec = VideoCodecs.hevc;
            Resolution = Resolutions.r1080p;
            BitRate = 1000;
            FPS = 30;
            RemoveAudio = false;
        }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine("Video:");
            sb.AppendLine("\tCodec:          " + Codec);
            sb.AppendLine("\tFPS:            " + FPS);
            sb.AppendLine("\tResolution:     " + EnumHelper.GetResolution(Resolution));
            sb.AppendLine("\tBitRate:        " + BitRate);
            sb.AppendLine("\tRemove Audio:   " + RemoveAudio);
            return sb.ToString();
        }
    }
}
