namespace MediaTransCoder.Backend {
    /// <summary>
    /// Suported video resolutions
    /// </summary>
    public enum Resolutions {
        //TODO: Check resolution and rethink if they should not be restricted to 16:9
        [Resolution(256, 144, "144p")]
        r144p,
        [Resolution(320, 180, "180p")]
        r180p,
        [Resolution(320, 240, "240p")]
        r240p,
        [Resolution(640, 360, "360p")]
        r360p,
        [Resolution(720, 480, "480p")]
        r480p,
        [Resolution(960, 540, "540p")]
        r540p,
        [Resolution(1280, 720, "HD (720p)")]
        r720p,
        [Resolution(1920, 1080, "FullHD (1080p)")]
        r1080p,
        [Resolution(2560, 1440, "QHD (1440p)")]
        r1440p,
        [Resolution(3840, 2160, "UHD 4K (2160p)")]
        r2160p,
        [Resolution(7680, 4320, "UHD 8K (4320p)")]
        r4320p
    }
}
