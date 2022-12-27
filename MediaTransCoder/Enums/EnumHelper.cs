using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MediaTransCoder.Backend {
    public static class EnumHelper {
        public static string GetCommand(AudioCodecs val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName() ?? val.ToString();
        }

        public static string GetName(AudioCodecs val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetDescription() ?? val.ToString();
        }

        public static string GetCommand(VideoCodecs val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName() ?? val.ToString();
        }
        public static string GetName(VideoCodecs val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetDescription() ?? val.ToString();
        }
        public static string GetCommand(ContainerFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName() ?? val.ToString();
        }

        public static string GetName(ContainerFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetDescription() ?? val.ToString();
        }

        public static string GetFfmpegLoggingLevel(LoggingLevel val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName() ?? val.ToString();
        }

        public static string GetResolution(Resolutions val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ResolutionAttribute>()?
                .GetResolution() ?? val.ToString();
        }

        public static string GetName(Resolutions val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ResolutionAttribute>()?
                .Name ?? val.ToString();
        }
    }
}
