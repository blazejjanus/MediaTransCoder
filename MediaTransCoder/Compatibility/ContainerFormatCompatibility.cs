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

        public static List<ContainerFormat> GetCompatibleFormats(ContainerFormats vcodec) {
            var result = new List<ContainerFormat>();
            switch (vcodec) {
                case ContainerFormats.vp9:
                    result.Add(ContainerFormat.webm);
                    break;
                case ContainerFormats.vp8:
                    result.Add(ContainerFormat.webm);
                    result.Add(ContainerFormat.matroska);
                    break;
                case ContainerFormats.h264:
                    result.Add(ContainerFormat.c3gp);
                    result.Add(ContainerFormat.flv);
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    break;
                case ContainerFormats.mpeg4:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    break;
                case ContainerFormats.ffv1:
                    result.Add(ContainerFormat.matroska);
                    break;
                case ContainerFormats.gifv:
                    result.Add(ContainerFormat.matroska);
                    break;
                case ContainerFormats.h263p:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    break;
                case ContainerFormats.hevc:
                    result.Add(ContainerFormat.matroska);
                    break;
                case ContainerFormats.msmpeg4v3:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    break;
                case ContainerFormats.prores:
                    result.Add(ContainerFormat.matroska);
                    break;
                case ContainerFormats.rawvideo:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    break;
                case ContainerFormats.wmv1:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    result.Add(ContainerFormat.asf);
                    break;
                case ContainerFormats.wmv2:
                    result.Add(ContainerFormat.matroska);
                    result.Add(ContainerFormat.avi);
                    result.Add(ContainerFormat.asf);
                    break;
                case ContainerFormats.msvideo1:
                    result.Add(ContainerFormat.asf);
                    break;
            }
            return result;
        }

        public static ContainerFormat GetDefaultFormat(ContainerFormats vcodec) {
            return GetCompatibleFormats(vcodec).First();
        }
    }
}
