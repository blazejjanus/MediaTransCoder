namespace MediaTransCoder.Backend {
    public static class PresetsService {
        public static Preset Get(PresetType type, PresetTarget target, PresetQuality quality) {
            switch (type) {
                case PresetType.AUDIO:
                    return GetAudio(target, quality);
                case PresetType.VIDEO:
                    return GetVideo(target, quality);
                case PresetType.IMAGE:
                    return GetImage(target, quality);
                default:
                    throw new ArgumentException("type");
            }
        }

        public static Preset GetAudio(PresetTarget target, PresetQuality quality) {
            var result = new Preset(PresetType.AUDIO);
            switch (target) {
                case PresetTarget.SIZE:
                    switch (quality) {
                        case PresetQuality.ULTRA:

                            break;
                        case PresetQuality.HIGH:

                            break;
                        case PresetQuality.MEDIUM:

                            break;
                        case PresetQuality.LOW:

                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.QUALITY:
                    switch (quality) {
                        case PresetQuality.ULTRA:

                            break;
                        case PresetQuality.HIGH:

                            break;
                        case PresetQuality.MEDIUM:

                            break;
                        case PresetQuality.LOW:

                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.SPEED:
                    switch (quality) {
                        case PresetQuality.ULTRA:

                            break;
                        case PresetQuality.HIGH:

                            break;
                        case PresetQuality.MEDIUM:

                            break;
                        case PresetQuality.LOW:

                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                default:
                    throw new ArgumentException("target");
            }
            return result;
        }

        public static Preset GetVideo(PresetTarget target, PresetQuality quality) {
            var result = new Preset(PresetType.AUDIO);
            switch (target) {
                case PresetTarget.SIZE:
                    switch (quality) {
                        case PresetQuality.ULTRA:

                            break;
                        case PresetQuality.HIGH:

                            break;
                        case PresetQuality.MEDIUM:

                            break;
                        case PresetQuality.LOW:

                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.QUALITY:
                    switch (quality) {
                        case PresetQuality.ULTRA:

                            break;
                        case PresetQuality.HIGH:

                            break;
                        case PresetQuality.MEDIUM:

                            break;
                        case PresetQuality.LOW:

                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.SPEED:
                    switch (quality) {
                        case PresetQuality.ULTRA:

                            break;
                        case PresetQuality.HIGH:

                            break;
                        case PresetQuality.MEDIUM:

                            break;
                        case PresetQuality.LOW:

                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                default:
                    throw new ArgumentException("target");
            }
            return result;
        }

        public static Preset GetImage(PresetTarget target, PresetQuality quality) {
            var result = new Preset(PresetType.AUDIO);
            switch (target) {
                case PresetTarget.SIZE:
                    switch (quality) {
                        case PresetQuality.ULTRA:

                            break;
                        case PresetQuality.HIGH:

                            break;
                        case PresetQuality.MEDIUM:

                            break;
                        case PresetQuality.LOW:

                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.QUALITY:
                    switch (quality) {
                        case PresetQuality.ULTRA:

                            break;
                        case PresetQuality.HIGH:

                            break;
                        case PresetQuality.MEDIUM:

                            break;
                        case PresetQuality.LOW:

                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.SPEED:
                    switch (quality) {
                        case PresetQuality.ULTRA:

                            break;
                        case PresetQuality.HIGH:

                            break;
                        case PresetQuality.MEDIUM:

                            break;
                        case PresetQuality.LOW:

                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                default:
                    throw new ArgumentException("target");
            }
            return result;
        }
    }
}
