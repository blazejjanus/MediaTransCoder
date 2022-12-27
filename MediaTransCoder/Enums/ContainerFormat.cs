namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported container formats. Use Name to get command, use Description to get dispalyed name.
    /// </summary>
    public enum ContainerFormat {
        [ContainerFormat("webm")]
        webm,
        [ContainerFormat("wav", "wave")]
        wav,
        [ContainerFormat("3gp")]
        c3gp,
        [ContainerFormat("flv", "FlashVideo")]
        flv,
        [ContainerFormat("matroska")]
        matroska,
        [ContainerFormat("ogg")]
        ogg,
        [ContainerFormat("avi")]
        avi
    }
}
