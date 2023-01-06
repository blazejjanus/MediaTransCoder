namespace MediaTransCoder.Backend {
    public static partial class Compatibility {
        public static List<ContainerFormat> GetCompatibleFormats(AudioCodecs acodec) {
            var result = new List<ContainerFormat>();
            switch (acodec) {
                case AudioCodecs.libopus:
                    result.Add(ContainerFormat.ogg);
                    result.Add(ContainerFormat.webm);
                    break;
                case AudioCodecs.libvorbis:
                    result.Add(ContainerFormat.ogg);
                    result.Add(ContainerFormat.webm);
                    break;
                case AudioCodecs.pcm:
                    result.Add(ContainerFormat.wav);
                    result.Add(ContainerFormat.avi);
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.asf);
                    break;
                case AudioCodecs.aac:
                    result.Add(ContainerFormat.c3gp);
                    result.Add(ContainerFormat.flv);
                    break;
                case AudioCodecs.mp3:
                    result.Add(ContainerFormat.avi);
                    result.Add(ContainerFormat.flv);
                    result.Add(ContainerFormat.matroska);
                    break;
                case AudioCodecs.flac:
                    result.Add(ContainerFormat.matroska);
                    break;
                case AudioCodecs.ac3:
                    result.Add(ContainerFormat.avi);
                    result.Add(ContainerFormat.matroska);
                    break;
                case AudioCodecs.eac3:
                    result.Add(ContainerFormat.matroska);
                    break;
                case AudioCodecs.wmav1:
                    result.Add(ContainerFormat.avi);
                    result.Add(ContainerFormat.asf);
                    break;
                case AudioCodecs.wmav2:
                    result.Add(ContainerFormat.avi);
                    result.Add(ContainerFormat.asf);
                    break;
            }
            return result;
        }

        public static ContainerFormat GetDefaultFormat(AudioCodecs acodec) {
            return GetCompatibleFormats(acodec).First();
        }

        public static List<ContainerFormat> GetCompatibleFormats(VideoCodecs vcodec) {
            var result = new List<ContainerFormat>();
            switch (vcodec) {
                case VideoCodecs.vp9:
                    result.Add(ContainerFormat.webm);
                    break;
                case VideoCodecs.vp8:
                    result.Add(ContainerFormat.webm);
                    result.Add(ContainerFormat.matroska);
                    break;
                case VideoCodecs.h264:
                    result.Add(ContainerFormat.c3gp);
                    result.Add(ContainerFormat.flv);
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    break;
                case VideoCodecs.mpeg4:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    break;
                case VideoCodecs.ffv1:
                    result.Add(ContainerFormat.matroska);
                    break;
                case VideoCodecs.gifv:
                    result.Add(ContainerFormat.matroska);
                    break;
                case VideoCodecs.h263p:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    break;
                case VideoCodecs.hevc:
                    result.Add(ContainerFormat.matroska);
                    break;
                case VideoCodecs.msmpeg4v3:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    break;
                case VideoCodecs.prores:
                    result.Add(ContainerFormat.matroska);
                    break;
                case VideoCodecs.rawvideo:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    break;
                case VideoCodecs.wmv1:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    result.Add(ContainerFormat.asf);
                    break;
                case VideoCodecs.wmv2:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    result.Add(ContainerFormat.asf);
                    break;
                case VideoCodecs.msvideo1:
                    result.Add(ContainerFormat.asf);
                    break;
            }
            return result;
        }

        public static ContainerFormat GetDefaultFormat(VideoCodecs vcodec) {
            return GetCompatibleFormats(vcodec).First();
        }
    }
}
