namespace MediaTransCoder.Backend {
    public static partial class Compatibility {
        public static List<VideoCodecs> GetCompatibleVideoCodecs(ContainerFormat format) {
            var result = new List<VideoCodecs>();
            switch (format) {
                case ContainerFormat.webm:
                    result.Add(VideoCodecs.vp9);
                    result.Add(VideoCodecs.vp8);
                    break;
                case ContainerFormat.wav:
                    //Nothing - audio only
                    break;
                case ContainerFormat.c3gp:
                    result.Add(VideoCodecs.h264);
                    break;
                case ContainerFormat.flv:
                    result.Add(VideoCodecs.h264);
                    break;
                case ContainerFormat.matroska:
                    result.Add(VideoCodecs.mpeg4);
                    result.Add(VideoCodecs.ffv1);
                    result.Add(VideoCodecs.gifv);
                    result.Add(VideoCodecs.h263p);
                    result.Add(VideoCodecs.h264);
                    result.Add(VideoCodecs.hevc);
                    result.Add(VideoCodecs.msmpeg4v3);
                    result.Add(VideoCodecs.prores);
                    result.Add(VideoCodecs.rawvideo);
                    result.Add(VideoCodecs.vp8);
                    result.Add(VideoCodecs.vp9);
                    result.Add(VideoCodecs.wmv1);
                    result.Add(VideoCodecs.wmv2);
                    break;
                case ContainerFormat.ogg:
                    //Nothing - audio only
                    break;
                case ContainerFormat.avi:
                    result.Add(VideoCodecs.mpeg4);
                    result.Add(VideoCodecs.h263p);
                    result.Add(VideoCodecs.h264);
                    result.Add(VideoCodecs.msmpeg4v3);
                    result.Add(VideoCodecs.msvideo1);
                    result.Add(VideoCodecs.rawvideo);
                    result.Add(VideoCodecs.wmv1);
                    result.Add(VideoCodecs.wmv2);
                    break;
                case ContainerFormat.asf:
                    result.Add(VideoCodecs.msvideo1);
                    break;
            }
            return result;
        }

        public static VideoCodecs? GetDefaultVideoCodec(ContainerFormat format) {
            var result = GetCompatibleVideoCodecs(format);
            if (result.Count > 0) {
                return result.First();
            } else {
                return null;
            }
        }
    }
}
