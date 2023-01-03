namespace MediaTransCoder.Backend {
    public static class CompatibilityInfo {
        public static List<AudioCodecs> GetCompatibleAudioCodecs(ContainerFormat format) {
            var result = new List<AudioCodecs>();
            switch (format) {
                case ContainerFormat.webm:
                    result.Add(AudioCodecs.libvorbis);
                    result.Add(AudioCodecs.libopus);
                    result.Add(AudioCodecs.flac);
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
                    result.Add(AudioCodecs.pcm);
                    result.Add(AudioCodecs.mp3);
                    result.Add(AudioCodecs.libvorbis);
                    result.Add(AudioCodecs.libopus);
                    result.Add(AudioCodecs.flac);
                    result.Add(AudioCodecs.ac3);
                    result.Add(AudioCodecs.eac3);
                    break;
                case ContainerFormat.ogg:
                    result.Add(AudioCodecs.libvorbis);
                    result.Add(AudioCodecs.libopus);
                    break;
                case ContainerFormat.avi:
                    result.Add(AudioCodecs.pcm);
                    result.Add(AudioCodecs.mp3);
                    result.Add(AudioCodecs.ac3);
                    result.Add(AudioCodecs.wmav1);
                    result.Add(AudioCodecs.wmav2);
                    break;
                case ContainerFormat.mpeg4:
                    result.Add(AudioCodecs.mp3);
                    result.Add(AudioCodecs.aac);
                    result.Add(AudioCodecs.libvorbis);
                    break;
                case ContainerFormat.asf:
                    result.Add(AudioCodecs.wmav1);
                    result.Add(AudioCodecs.wmav2);
                    result.Add(AudioCodecs.pcm);
                    break;
            }
            return result;
        }

        public static List<VideoCodecs> GetCompatibleVideoCodecs(ContainerFormat format) {
            var result = new List<VideoCodecs>();
            switch (format) {
                case ContainerFormat.webm:
                    result.Add(VideoCodecs.vp8);
                    result.Add(VideoCodecs.vp9);
                    result.Add(VideoCodecs.webp);
                    break;
                case ContainerFormat.wav:
                    //Nothing - audio only
                    break;
                case ContainerFormat.c3gp:
                    result.Add(VideoCodecs.h263);
                    result.Add(VideoCodecs.h263p);
                    result.Add(VideoCodecs.h264);
                    break;
                case ContainerFormat.flv:
                    result.Add(VideoCodecs.h263);
                    result.Add(VideoCodecs.h263p);
                    result.Add(VideoCodecs.h264);
                    break;
                case ContainerFormat.matroska:
                    result.Add(VideoCodecs.dvvideo);
                    result.Add(VideoCodecs.ffv1);
                    result.Add(VideoCodecs.gif);
                    result.Add(VideoCodecs.h263);
                    result.Add(VideoCodecs.h263p);
                    result.Add(VideoCodecs.h264);
                    result.Add(VideoCodecs.hevc);
                    result.Add(VideoCodecs.mpeg4);
                    result.Add(VideoCodecs.msmpeg4v3);
                    result.Add(VideoCodecs.prores);
                    result.Add(VideoCodecs.rawvideo);
                    result.Add(VideoCodecs.vp8);
                    result.Add(VideoCodecs.vp9);
                    result.Add(VideoCodecs.webp);
                    result.Add(VideoCodecs.wmv1);
                    result.Add(VideoCodecs.wmv2);
                    result.Add(VideoCodecs.zmbv);
                    result.Add(VideoCodecs.zlib);
                    break;
                case ContainerFormat.ogg:
                    //Nothing - audio only
                    break;
                case ContainerFormat.avi:
                    result.Add(VideoCodecs.h263);
                    result.Add(VideoCodecs.h263p);
                    result.Add(VideoCodecs.h264);
                    result.Add(VideoCodecs.mpeg4);
                    result.Add(VideoCodecs.msmpeg4v3);
                    result.Add(VideoCodecs.msvideo1);
                    result.Add(VideoCodecs.rawvideo);
                    result.Add(VideoCodecs.wmv1);
                    result.Add(VideoCodecs.wmv2);
                    break;
                case ContainerFormat.mpeg4:
                    result.Add(VideoCodecs.h263);
                    result.Add(VideoCodecs.h263p);
                    result.Add(VideoCodecs.h264);
                    result.Add(VideoCodecs.hevc);
                    result.Add(VideoCodecs.mpeg4);
                    result.Add(VideoCodecs.msmpeg4v3);
                    result.Add(VideoCodecs.prores);
                    result.Add(VideoCodecs.rv20);
                    result.Add(VideoCodecs.wmv1);
                    result.Add(VideoCodecs.wmv2);
                    result.Add(VideoCodecs.zmbv);
                    result.Add(VideoCodecs.zlib);
                    break;
                case ContainerFormat.asf:
                    result.Add(VideoCodecs.msvideo1);
                    result.Add(VideoCodecs.rv20);
                    break;
            }
            return result;
        }
    }
}
