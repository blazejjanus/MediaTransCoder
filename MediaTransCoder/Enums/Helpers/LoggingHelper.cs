using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MediaTransCoder.Backend {
    public static partial class EnumHelper {
        public static string GetFfmpegLoggingLevel(LoggingLevel val) {
            return val.GetType().GetMember(val.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName() ?? val.ToString();
        }
    }
}
