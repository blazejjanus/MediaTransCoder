namespace MediaTransCoder.Backend {
    internal static class Logging {

        public static bool IsDebug {
            get {
                if (Context.IsSet) {
                    if(context != null && context.IsDebug != null) {
                        return context.IsDebug.Value; //Context setting override everything
                    }
                }
                if (context?.Config.Environment == EnvironmentType.Production) {
                    if (System.Diagnostics.Debugger.IsAttached) {
                        return true;
                    }
                    return false;
                }
                return true;
            }
        }

        public static string LoggingLevel {
            get {
                if(IsDebug) {
                    return "warning";
                } else {
                    return "error";
                }
            }
        }

        private static Context context = Context.Get();

        internal static void Debug(string message) {
            if (IsDebug) {
                context.Display.Send(message);
            }
        }
    }
}
