namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        /// <summary>
        /// Return pixel format formated for compatibility with ffmpeg
        /// </summary>
        /// <param name="val">PixelFormat</param>
        /// <returns>Ffmpeg formatted PixelFormat</returns>
        public static string GetName(PixelFormats val) {
            return val.ToString().ToLower();
        }
    }
}
