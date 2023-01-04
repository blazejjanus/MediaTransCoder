using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        public static string GetName(PixelFormats val) {
            return val.ToString().ToLower();
        }
    }
}
