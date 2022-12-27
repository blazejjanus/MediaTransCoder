using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        public static string GetCommand(ContainerFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ContainerFormatAttribute>()?
                .Name ?? val.ToString();
        }

        public static string GetName(ContainerFormat val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ContainerFormatAttribute>()?
                .Description ?? val.ToString();
        }
    }
}
