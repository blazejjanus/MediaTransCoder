using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        public static string GetName(VideoCodecs val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<VideoCodecAttribute>()?
                .Name ?? val.ToString();
        }

        public static string GetDescription(VideoCodecs val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<VideoCodecAttribute>()?
                .Description ?? val.ToString();
        }

        public static string? GetFileExtension(VideoCodecs val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<VideoCodecAttribute>()?
                .FileExtension;
        }
    }
}