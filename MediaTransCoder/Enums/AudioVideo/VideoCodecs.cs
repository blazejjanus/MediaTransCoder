namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported video codecs. Use Name to get command, use Command to get dispalyed name.
    /// </summary>
    public enum VideoCodecs {
        [VideoCodec("ffv1", "FFV1", ".avi")]
        ffv1,
        [VideoCodec("gif", "GIF", ".gifv")]
        gif,
        [VideoCodec("h263p", "h263p")]
        h263p,
        [VideoCodec("h264", "h264")]
        h264,
        [VideoCodec("libx265", "HEVC")] 
        hevc,
        [VideoCodec("mpeg4", "MPEG-4", ".mp4")]
        mpeg4,
        [VideoCodec("msmpeg4v3", "Microsoft MPEG-4", ".mp4")]
        msmpeg4v3,
        [VideoCodec("msvideo1", "Microsoft Video", ".wmv")]
        msvideo1,
        [VideoCodec("prores", "Apple ProRes")]
        prores,
        [VideoCodec("rawvideo", "Raw Video", ".raw")] 
        rawvideo,
        [VideoCodec("vp8", "VP8")]
        vp8,
        [VideoCodec("vp9", "VP9")]
        vp9,
        [VideoCodec("wmv1", "WMV-1", ".wmv")]
        wmv1,
        [VideoCodec("wmv2", "WMV-2", ".wmv")]
        wmv2,
    }
}
