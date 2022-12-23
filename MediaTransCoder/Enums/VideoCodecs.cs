using System.ComponentModel.DataAnnotations;

namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported video codecs. Use Name to get command, use Description to get dispalyed name.
    /// </summary>
    public enum VideoCodecs {
        //Untested
        [Display(Name = "dvvideo", Description = "DigitalVideo")]
        dvvideo,
        [Display(Name = "ffv1", Description = "FFV1")]
        ffv1,
        [Display(Name = "gif", Description = "GIF")]
        gif,
        [Display(Name = "h263", Description = "h263")]
        h263,
        [Display(Name = "h263p", Description = "h263p")]
        h263p,
        [Display(Name = "h264", Description = "h264")]
        h264,
        [Display(Name = "libx265", Description = "HEVC (h265)")]
        hevc,
        [Display(Name = "mpeg4", Description = "MPEG-4")]
        mpeg4,
        [Display(Name = "msmpeg4v3", Description = "Microsoft MPEG-4")]
        msmpeg4v3,
        [Display(Name = "msvideo1", Description = "Microsoft Video")]
        msvideo1,
        [Display(Name = "prores", Description = "Apple ProRes")]
        prores,
        [Display(Name = "rawvideo", Description = "Raw Video")]
        rawvideo,
        [Display(Name = "rv20", Description = "RealVideo 2.0")]
        rv20,
        [Display(Name = "vp8", Description = "VP8")]
        vp8,
        [Display(Name = "vp9", Description = "VP9")]
        vp9,
        [Display(Name = "webp", Description = "Webp")]
        webp,
        [Display(Name = "wmv1", Description = "WMV-1")]
        wmv1,
        [Display(Name = "wmv2", Description = "WMV-2")]
        wmv2,
        [Display(Name = "zmbv", Description = "Zip Motion Blocks Video")]
        zmbv,
        [Display(Name = "zlib", Description = "LCL ZLib")]
        zlib
    }
}
