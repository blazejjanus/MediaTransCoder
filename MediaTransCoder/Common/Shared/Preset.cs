namespace MediaTransCoder.Backend {
    /// <summary>
    /// Class representing preset
    /// </summary>
    public class Preset {
        /// <summary>
        /// Preset Type - Audio, Video, Image
        /// </summary>
        public PresetType Type { get; private set; }
        /// <summary>
        /// Preset purpose - e.g. speed, quality, size
        /// </summary>
        public PresetTarget Target { get; private set; }
        /// <summary>
        /// Output quality
        /// </summary>
        public PresetQuality Quality { get; private set; }
        /// <summary>
        /// Endpoint options for preset
        /// </summary>
        public EndpointOptions Options { get; private set; }

        internal Preset(PresetType type) {
            Type = type;
            switch (type) {
                case PresetType.AUDIO:
                    Options = EndpointOptions.GetSampleAudioOptions();
                    break;
                case PresetType.VIDEO:
                    Options = EndpointOptions.GetSampleVideoOptions();
                    break;
                case PresetType.IMAGE:
                    Options = EndpointOptions.GetSampleImageOptions();
                    break;
                default:
                    Options = new EndpointOptions();
                    break;
            }
        }
    }
}
