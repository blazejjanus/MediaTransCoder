namespace MediaTransCoder.Backend {
    public static partial class Compatibility {
        public static List<ContainerFormats> GetCompatibleVideoCodecs(ContainerFormat format) {
            var result = new List<ContainerFormats>();
            switch (format) {
                case ContainerFormat.webm:
                    result.Add(ContainerFormats.vp9);
                    result.Add(ContainerFormats.vp8);
                    break;
                case ContainerFormat.wav:
                    //Nothing - audio only
                    break;
                case ContainerFormat.c3gp:
                    result.Add(ContainerFormats.h264);
                    break;
                case ContainerFormat.flv:
                    result.Add(ContainerFormats.h264);
                    break;
                case ContainerFormat.matroska:
                    result.Add(ContainerFormats.mpeg4);
                    result.Add(ContainerFormats.ffv1);
                    result.Add(ContainerFormats.gifv);
                    result.Add(ContainerFormats.h263p);
                    result.Add(ContainerFormats.h264);
                    result.Add(ContainerFormats.hevc);
                    result.Add(ContainerFormats.msmpeg4v3);
                    result.Add(ContainerFormats.prores);
                    result.Add(ContainerFormats.rawvideo);
                    result.Add(ContainerFormats.vp8);
                    result.Add(ContainerFormats.vp9);
                    result.Add(ContainerFormats.wmv1);
                    result.Add(ContainerFormats.wmv2);
                    break;
                case ContainerFormat.ogg:
                    //Nothing - audio only
                    break;
                case ContainerFormat.avi:
                    result.Add(ContainerFormats.mpeg4);
                    result.Add(ContainerFormats.h263p);
                    result.Add(ContainerFormats.h264);
                    result.Add(ContainerFormats.msmpeg4v3);
                    result.Add(ContainerFormats.msvideo1);
                    result.Add(ContainerFormats.rawvideo);
                    result.Add(ContainerFormats.wmv1);
                    result.Add(ContainerFormats.wmv2);
                    break;
                case ContainerFormat.asf:
                    result.Add(ContainerFormats.msvideo1);
                    break;
            }
            return result;
        }

        public static ContainerFormats? GetDefaultVideoCodec(ContainerFormat format) {
            var result = GetCompatibleVideoCodecs(format);
            if (result.Count > 0) {
                return result.First();
            } else {
                return null;
            }
        }
    }
}
