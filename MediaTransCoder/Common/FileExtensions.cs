namespace MediaTransCoder.Backend {
    public static class FileExtensions {
        public static List<string> GetAudioExtensions(bool searchCriteria = false) {
            var result = new List<string>();
            foreach(ContainerFormat format in Enum.GetValues(typeof(ContainerFormat))) {
                string? extension = EnumHelper.GetFileExtension(format, true);
                if (extension != null) {
                    if (!result.Contains(extension)) {
                        if (searchCriteria) {
                            result.Add("*" + extension);
                        } else {
                            result.Add(extension);
                        }
                    }
                }
            }
            foreach (AudioCodecs codec in Enum.GetValues(typeof(AudioCodecs))) {
                string? extension = EnumHelper.GetFileExtension(codec);
                if (extension != null) {
                    if (!result.Contains(extension)) {
                        if (searchCriteria) {
                            result.Add("*" + extension);
                        } else {
                            result.Add(extension);
                        }
                    }
                }
            }
            return result;
        }

        public static List<string> GetVideoExtensions(bool searchCriteria = false) {
            var result = new List<string>();
            foreach (ContainerFormat format in Enum.GetValues(typeof(ContainerFormat))) {
                string? extension = EnumHelper.GetFileExtension(format, false);
                if (extension != null) {
                    if (!result.Contains(extension)) {
                        if (searchCriteria) {
                            result.Add("*" + extension);
                        } else {
                            result.Add(extension);
                        }
                    }
                }
            }
            foreach (VideoCodecs codec in Enum.GetValues(typeof(VideoCodecs))) {
                string? extension = EnumHelper.GetFileExtension(codec);
                if (extension != null) {
                    if (!result.Contains(extension)) {
                        if (searchCriteria) {
                            result.Add("*" + extension);
                        } else {
                            result.Add(extension);
                        }
                    }
                }
            }
            return result;
        }
    }
}
