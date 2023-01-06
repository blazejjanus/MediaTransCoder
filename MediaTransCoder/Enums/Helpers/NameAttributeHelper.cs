using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        public static string GetName(SamplingFrequency val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<NameAttribute>()?
                .Name ?? val.ToString();
        }

        public static string GetDisplayedName(SamplingFrequency val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<NameAttribute>()?
                .Name ?? val.ToString();
        }

        public static string GetName(AudioBitRate val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<NameAttribute>()?
                .DisplayedName ?? val.ToString();
        }

        public static string GetDisplayedName(AudioBitRate val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<NameAttribute>()?
                .DisplayedName ?? val.ToString();
        }
    }
}
