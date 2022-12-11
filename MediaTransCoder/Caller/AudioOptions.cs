namespace MediaTransCoder.Backend
{
    public class AudioOptions
    {
        public AudioCodecs Codec { get; set; }
        public uint BitRate { get; set; }
        public AudioOptions() {
            Codec = AudioCodecs.mp3;
            BitRate = 192;
        }
    }
}
