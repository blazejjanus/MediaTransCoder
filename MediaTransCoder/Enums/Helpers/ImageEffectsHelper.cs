using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        public static string GetName(ImageEffects val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ImageEffectsAttribute>()?
                .Name ?? val.ToString();
        }

        public static string GetCommand(ImageEffects val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ImageEffectsAttribute>()?
                .Command ?? val.ToString();
        }
    }
}
