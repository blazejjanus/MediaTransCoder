namespace MediaTransCoder.Backend
{
    public class VideoOptions
    {
        public VideoCodecs Codec { get; set; }
        public Resolutions Resolution { get; set; }
        public uint BitRate {
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

        public uint FPS {
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
        
        private uint fps;
        private uint br;

        public string GetFormatedBitRate() {
            /*
            if (br >= 1000) {
                if(br%1000 < 100) {
                    return (br / 1000).ToString() + "m";
                } else {
                    return br.ToString() + "k";
                }
            } else {
                return br.ToString() + "k";
            }
            */
            return br.ToString() + "k";
        }
        public VideoOptions() {
            Codec = VideoCodecs.hevc;
            BitRate = 1000;
            FPS = 30;
        }
    }
}
