using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        public static string GetName(ContainerFormats val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<VideoCodecAttribute>()?
                .Name ?? val.ToString();
        }

        public static string GetDescription(ContainerFormats val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<VideoCodecAttribute>()?
                .Description ?? val.ToString();
        }

        public static string? GetFileExtension(ContainerFormats val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<VideoCodecAttribute>()?
                .FileExtension;
        }
    }
}