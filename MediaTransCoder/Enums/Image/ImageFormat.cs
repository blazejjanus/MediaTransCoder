namespace MediaTransCoder.Backend {
    public enum ImageFormat {
        [ImageFormat("bmp")]
        BMP,
        [ImageFormat("dpx")]
        DPX,
        [ImageFormat("gif")]
        GIF,
        [ImageFormat("jpg", new[] { ".jpeg", ".jpg"} )]
        JPG,
        [ImageFormat("pcx")]
        PCX,
        [ImageFormat("png")]
        PNG,
        [ImageFormat("ppm")]
        PPM,
        [ImageFormat("tga")]
        TGA,
        [ImageFormat("tiff")]
        TIFF
    }
}
