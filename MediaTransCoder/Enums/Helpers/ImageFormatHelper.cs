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

        public static string? GetFileExtension(ImageFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ImageFormatAttribute>()?
                .Extensions.First();
        }

        public static List<string>? GetFileExtensions(ImageFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ImageFormatAttribute>()?
                .Extensions;
        }
    }
}
