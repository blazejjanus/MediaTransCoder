﻿namespace MediaTransCoder.Backend {
    public static partial class Compatibility {
        public static List<Resolutions> GetCompatibleResolutions(VideoCodecs codec) {
            List<Resolutions> result = Enum.GetValues(typeof(Resolutions)).Cast<Resolutions>().ToList();
            if(codec == VideoCodecs.h263p) {
                result.Remove(Resolutions.r1440p);
                result.Remove(Resolutions.r2160p);
                result.Remove(Resolutions.r4320p);
            }
            return result;
        }
    }
}
