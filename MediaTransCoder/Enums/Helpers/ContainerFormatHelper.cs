using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        public static string GetName(ContainerFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ContainerFormatAttribute>()?
                .Name ?? val.ToString();
        }

        public static string GetDescription(ContainerFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ContainerFormatAttribute>()?
                .Description ?? val.ToString();
        }

        public static string? GetFileExtension(ContainerFormat val, bool audio = false) {
            var result = val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ContainerFormatAttribute>();
            if (audio) {
                return result?.AudioExtension;
            } else {
                return result?.VideoExtension;
            }
        }

        public static bool IsAudioOnly(ContainerFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ContainerFormatAttribute>()?
                .AudioOnly ?? false;
        }
    }
}
