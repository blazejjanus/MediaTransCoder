namespace MediaTransCoder.Backend
{
    public class EndpointArgs
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public bool Recursive { get; set; }
        public ContainerFormat Format { get; set; }
        //TODO: Shall store object representing all audio conversion settings
        public AudioCodecs? AudioCodec { get; set; }
        //TODO: Shall store object representing all video conversion settings
        public VideoCodecs? VideoCodec { get; set; }
    }
}
