using System.ComponentModel.DataAnnotations;

namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported video codecs. Use Name to get command, use Description to get dispalyed name.
    /// </summary>
    public enum VideoCodecs {
        [VideoCodec("dvvideo", "DigitalVideo", ".mov")]
        dvvideo,
        [VideoCodec("ffv1", "FFV1", ".avi")]
        ffv1,
        [VideoCodec("gif", "GIF", ".gif")]
        gif,
        [VideoCodec("h263", "h263")]
        h263,
        [VideoCodec("h263p", "h263p")]
        h263p,
        [Display(Name = "h264", Description = "h264")]
        [VideoCodec("h264", "h264")]
        h264,
        [VideoCodec("libx265", "libx265")] 
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
        [VideoCodec("rv20", "RealVideo 2.0")]
        rv20,
        [VideoCodec("vp8", "VP8")]
        vp8,
        [VideoCodec("dvvideo", "DigitalVideo")]
        vp9,
        [VideoCodec("webp", "Webp", ".webm")]
        webp,
        [VideoCodec("wmv1", "WMV-1", ".wmv")]
        wmv1,
        [VideoCodec("wmv2", "WMV-2", ".wmv")]
        wmv2,
        [VideoCodec("zmbv", "Zip Motion Blocks Video")]
        zmbv,
        [VideoCodec("zlib", "LCL ZLib")]
        zlib
    }
}
