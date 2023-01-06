namespace MediaTransCoder.Backend {
    /// <summary>
    /// Audio sampling frequency
    /// </summary>
    public enum SamplingFrequency {
        [Name("8 kHz")]
        ar8k = 8000,
        [Name("11 kHz")]
        ar11k = 11025,
        [Name("22 kHz")]
        ar22k = 22050,
        [Name("44,1 kHz")]
        ar44k = 44100,
        [Name("48 kHz")]
        ar48k = 48000,
        [Name("96 kHz")]
        ar96k = 96000,
        [Name("192 kHz")]
        ar192k = 192000
    }
}
