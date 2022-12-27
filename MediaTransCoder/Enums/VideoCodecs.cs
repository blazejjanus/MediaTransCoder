using System.ComponentModel.DataAnnotations;

namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported video codecs. Use Name to get command, use Description to get dispalyed name.
    /// </summary>
    public enum VideoCodecs {
        //Untested
        [VideoCodec("dvvideo", "DigitalVideo", ".mov")]
        dvvideo,
        [VideoCodec("ffv1", "FFV1", ".avi")]
        ffv1,
        [VideoCodec("gif", "GIF", ".gif")]
        gif,
        [VideoCodec("h263", "h263", ".mp4")] //Rather container extension
        h263,
        [VideoCodec("h263p", "h263p", ".mp4")] //Rather container extension
        h263p,
        [Display(Name = "h264", Description = "h264")]
        [VideoCodec("h264", "h264", ".mp4")] //Rather container extension
        h264,
        [VideoCodec("libx265", "HEVC (h265)", ".mp4")] //Rather container extension
        hevc,
        [VideoCodec("mpeg4", "MPEG-4", ".mp4")]
        mpeg4,
        [VideoCodec("msmpeg4v3", "Microsoft MPEG-4", ".mp4")]
        msmpeg4v3,
        [VideoCodec("msvideo1", "Microsoft Video", ".wmv")]
        msvideo1,
        [VideoCodec("prores", "Apple ProRes", ".mov")] //Rather container extension
        prores,
        [VideoCodec("rawvideo", "Raw Video", ".raw")] 
        rawvideo,
        [VideoCodec("rv20", "RealVideo 2.0", ".mov")] //Rather container extension
        rv20,
        [VideoCodec("vp8", "VP8", ".webm")] //Rather container extension
        vp8,
        [VideoCodec("dvvideo", "DigitalVideo", ".mov")] //Rather container extension
        vp9,
        [VideoCodec("webp", "Webp", ".webm")]
        webp,
        [VideoCodec("wmv1", "WMV-1", ".wmv")]
        wmv1,
        [VideoCodec("wmv2", "WMV-2", ".wmv")]
        wmv2,
        [VideoCodec("zmbv", "Zip Motion Blocks Video", ".mov")] //Rather container extension
        zmbv,
        [VideoCodec("zlib", "LCL ZLib", ".mov")] //Rather container extension
        zlib
    }
}
