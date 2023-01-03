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

        public static bool IsAudioFormat(ContainerFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ContainerFormatAttribute>()?
                .IsAudioFormat ?? false;
        }

        public static bool IsVideoFormat(ContainerFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ContainerFormatAttribute>()?
                .IsVideoFormat ?? false;
        }

        public static List<ContainerFormat> GetVideoFormats() {
            var result = new List<ContainerFormat>();
            foreach(ContainerFormat format in Enum.GetValues(typeof(ContainerFormat))) {
                if(IsVideoFormat(format)) {
                    result.Add(format);
                }
            }
            return result;
        }

        public static List<ContainerFormat> GetAudioFormats() {
            var result = new List<ContainerFormat>();
            foreach (ContainerFormat format in Enum.GetValues(typeof(ContainerFormat))) {
                if (IsAudioFormat(format)) {
                    result.Add(format);
                }
            }
            return result;
        }
    }
}
