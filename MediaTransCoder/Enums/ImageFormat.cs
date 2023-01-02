namespace MediaTransCoder.Backend {
    public enum ImageFormat {
        [ImageFormat("bmp")]
        BMP,
        [ImageFormat("dpx")]
        DPX,
        [ImageFormat("gif")]
        GIF,
        [ImageFormat("jpeg", new[] { ".jpeg", ".jpg"} )]
        JPEG,
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
