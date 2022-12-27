using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        public static string GetName(AudioCodecs val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<AudioCodecAttribute>()?
                .Name ?? val.ToString();
        }

        public static string GetDescription(AudioCodecs val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<AudioCodecAttribute>()?
                .Description ?? val.ToString();
        }

        public static string GetFileExtension(AudioCodecs val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<AudioCodecAttribute>()?
                .FileExtension ?? val.ToString();
        }
    }
}