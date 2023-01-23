using System.Numerics;
using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        public static string GetResolution(Resolutions val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ResolutionAttribute>()?
                .GetResolution() ?? val.ToString();
        }

        public static Vector2 GetResolutionValue(Resolutions val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ResolutionAttribute>()?
                .Size ?? new Vector2(1920, 1080);
        }

        public static string GetName(Resolutions val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ResolutionAttribute>()?
                .Name ?? val.ToString();
        }
    }
}
