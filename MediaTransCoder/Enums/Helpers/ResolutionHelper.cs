using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
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
