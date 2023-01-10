using System.Text;

namespace MediaTransCoder.Backend {
    public class VideoOptions {
        //TODO: Get propper values by codec
        /// <summary>
        /// Video codec
        /// </summary>
        public ContainerFormats Codec { get; set; }
        /// <summary>
        /// Video resolution
        /// </summary>
        public Resolutions Resolution { get; set; }
        /// <summary>
        /// Video bitrate
        /// </summary>
        public int BitRate { get; set; }

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

        public VideoOptions() {
            Codec = ContainerFormats.hevc;
            Resolution = Resolutions.r1080p;
            BitRate = 1000;
            FPS = 30;
            RemoveAudio = false;
        }

        public void CalcBitRate() {
            switch (Resolution) {
                case Resolutions.r144p:
                    BitRate = 100 / 30 * FPS;
                    break;
                case Resolutions.r180p:
                    BitRate = 150 / 30 * FPS;
                    break;
                case Resolutions.r240p:
                    BitRate = 200 / 30 * FPS;
                    break;
                case Resolutions.r360p:
                    BitRate = 300 / 30 * FPS;
                    break;
                case Resolutions.r480p:
                    BitRate = 400 / 30 * FPS;
                    break;
                case Resolutions.r540p:
                    BitRate = 450 / 30 * FPS;
                    break;
                case Resolutions.r720p:
                    BitRate = 600 / 30 * FPS;
                    break;
                case Resolutions.r1080p:
                    BitRate = 1400 / 30 * FPS;
                    break;
                case Resolutions.r1440p:
                    BitRate = 1800 / 30 * FPS;
                    break;
                case Resolutions.r2160p:
                    BitRate = 3000 / 30 * FPS;
                    break;
                case Resolutions.r4320p:
                    BitRate = 6000 / 30 * FPS;
                    break;
            }
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
