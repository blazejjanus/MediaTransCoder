namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported image formats
    /// </summary>
    public enum ImageFormat {
        [ImageFormat("bmp")]
        BMP,
        [ImageFormat("dpx")]
        DPX,
        [ImageFormat("gifv")]
        GIF,
        [ImageFormat("jpeg", "mjpeg", new[] { ".jpg", ".jpeg"} )]
        JPG,
        [ImageFormat("jpeg2000", "jpeg2000", new[] { ".jpg", ".jpeg"} )]
        JPG2000,
        [ImageFormat("pcx")]
        PCX,
        [ImageFormat("png")]
        PNG,
        [ImageFormat("ppm")]
        PPM,
        [ImageFormat("tga", "targa")]
        TGA,
        [ImageFormat("tiff")]
        TIFF
    }
}
