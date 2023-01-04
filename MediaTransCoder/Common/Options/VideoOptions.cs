using System.Text;

namespace MediaTransCoder.Backend {
    public class VideoOptions {
        //TODO: Get propper values by codec
        public VideoCodecs Codec { get; set; }
        public Resolutions Resolution { get; set; }
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
