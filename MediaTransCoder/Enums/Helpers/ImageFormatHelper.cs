using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        public static string GetName(ImageFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ImageFormatAttribute>()?
                .Name ?? val.ToString();
        }

        public static string GetCommand(ImageFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ImageFormatAttribute>()?
                .Command ?? val.ToString();
        }

        /// <summary>
        /// Returns default image format extension
        /// </summary>
        /// <param name="val">Image format</param>
        /// <returns>Default extension for provided format</returns>
        public static string? GetFileExtension(ImageFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ImageFormatAttribute>()?
                .Extensions.First();
        }

        /// <summary>
        /// Returns list of image format extensions
        /// </summary>
        /// <param name="val">Image format</param>
        /// <returns>File extensions for provided format</returns>
        public static List<string>? GetFileExtensions(ImageFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ImageFormatAttribute>()?
                .Extensions;
        }
    }
}
