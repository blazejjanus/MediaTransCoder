namespace MediaTransCoder.Backend {
    public enum Resolutions {
        //TODO: Check resolution and rethink if they should not be restricted to 16:9
        [Resolution(256, 144)]
        r144p,
        [Resolution(320, 180)]
        r180p,
        [Resolution(320, 240)]
        r240p,
        [Resolution(640, 360)]
        r360p,
        [Resolution(720, 480)]
        r480p,
        [Resolution(960, 540)]
        r540p,
        [Resolution(1290, 720, "HD")]
        r720p,
        [Resolution(1920, 1080, "FullHD")]
        r1080p,
        [Resolution(2560, 1440, "QHD")]
        r1440p,
        [Resolution(3840, 2160, "UHD 4K")]
        r2160p,
        [Resolution(7680, 4320, "UHD 8K")]
        r4320p
    }
}
