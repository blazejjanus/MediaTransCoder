namespace MediaTransCoder.Backend {
    public static partial class Compatibility {
        public static List<AudioCodecs> GetCompatibleAudioCodecs(ContainerFormat format) {
            var result = new List<AudioCodecs>();
            switch (format) {
                case ContainerFormat.webm:
                    result.Add(AudioCodecs.libopus);
                    result.Add(AudioCodecs.libvorbis);
                    break;
                case ContainerFormat.wav:
                    result.Add(AudioCodecs.pcm);
                    break;
                case ContainerFormat.c3gp:
                    result.Add(AudioCodecs.aac);
                    break;
                case ContainerFormat.flv:
                    result.Add(AudioCodecs.mp3);
                    result.Add(AudioCodecs.aac);
                    break;
                case ContainerFormat.matroska:
                    result.Add(AudioCodecs.mp3);
                    result.Add(AudioCodecs.pcm);
                    result.Add(AudioCodecs.libvorbis);
                    result.Add(AudioCodecs.libopus);
                    result.Add(AudioCodecs.flac);
                    result.Add(AudioCodecs.ac3);
                    result.Add(AudioCodecs.eac3);
                    break;
                case ContainerFormat.ogg:
                    result.Add(AudioCodecs.libopus);
                    result.Add(AudioCodecs.libvorbis);
                    break;
                case ContainerFormat.avi:
                    result.Add(AudioCodecs.mp3);
                    result.Add(AudioCodecs.pcm);
                    result.Add(AudioCodecs.ac3);
                    result.Add(AudioCodecs.wmav1);
                    result.Add(AudioCodecs.wmav2);
                    break;
                case ContainerFormat.asf:
                    result.Add(AudioCodecs.wmav2);
                    result.Add(AudioCodecs.wmav1);
                    result.Add(AudioCodecs.pcm);
                    break;
            }
            return result;
        }

        public static AudioCodecs GetDefaultAudioCodec(ContainerFormat format) {
            return GetCompatibleAudioCodecs(format).First();
        }
    }
}
