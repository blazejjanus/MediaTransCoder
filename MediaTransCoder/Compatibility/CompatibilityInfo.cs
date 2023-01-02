namespace MediaTransCoder.Backend {
    internal static class CompatibilityInfo {
        internal static List<AudioCodecs> GetCompatibleAudioCodecs(ContainerFormat format) {
            var result = new List<AudioCodecs>();
            switch (format) {
                case ContainerFormat.webm:
                    break;
                case ContainerFormat.wav:
                    break;
                case ContainerFormat.c3gp:
                    break;
                case ContainerFormat.flv:
                    break;
                case ContainerFormat.matroska:
                    break;
                case ContainerFormat.ogg:
                    break;
                case ContainerFormat.avi:
                    break;
            }
            return result;
        }
    }
}
