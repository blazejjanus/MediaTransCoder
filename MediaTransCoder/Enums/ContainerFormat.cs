namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported container formats. Use Name to get command, use Description to get dispalyed name.
    /// </summary>
    public enum ContainerFormat {
        [ContainerFormat("webm", "Webm", ".webm")]
        webm,
        [ContainerFormat("wav", "wave", ".wav", true)]
        wav,
        [ContainerFormat("3gp", "3GP", ".3gp")]
        c3gp,
        [ContainerFormat("flv", "FlashVideo")]
        flv,
        [ContainerFormat(Name = "matroska", Description = "Matroska", VideoExtension = ".mkv", AudioExtension = ".mka", AudioOnly = false)]
        matroska,
        [ContainerFormat("ogg", "ogg", ".ogg", true)]
        ogg,
        [ContainerFormat("avi")]
        avi
    }
}
