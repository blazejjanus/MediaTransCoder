using System.Numerics;

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
                    if (result.Options.Audio == null) {
                        result.Options.Audio = new AudioOptions();
                    }
                    result.Options.Format = ContainerFormat.ogg;
                    result.Options.Audio.Codec = AudioCodecs.libvorbis;
                    switch (quality) {
                        case PresetQuality.ULTRA:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar192k;
                            result.Options.Audio.BitRate = AudioBitRate.abr320;
                            break;
                        case PresetQuality.HIGH:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar96k;
                            result.Options.Audio.BitRate = AudioBitRate.abr256;
                            break;
                        case PresetQuality.MEDIUM:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar48k;
                            result.Options.Audio.BitRate = AudioBitRate.abr192;
                            break;
                        case PresetQuality.LOW:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar44k;
                            result.Options.Audio.BitRate = AudioBitRate.abr128;
                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.QUALITY:
                    if (result.Options.Audio == null) {
                        result.Options.Audio = new AudioOptions();
                    }
                    result.Options.Format = ContainerFormat.ogg;
                    result.Options.Audio.Codec = AudioCodecs.libopus;
                    switch (quality) {
                        case PresetQuality.ULTRA:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar192k;
                            result.Options.Audio.BitRate = AudioBitRate.abr320;
                            break;
                        case PresetQuality.HIGH:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar96k;
                            result.Options.Audio.BitRate = AudioBitRate.abr256;
                            break;
                        case PresetQuality.MEDIUM:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar48k;
                            result.Options.Audio.BitRate = AudioBitRate.abr192;
                            break;
                        case PresetQuality.LOW:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar44k;
                            result.Options.Audio.BitRate = AudioBitRate.abr128;
                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.SPEED:
                    if (result.Options.Audio == null) {
                        result.Options.Audio = new AudioOptions();
                    }
                    result.Options.Format = ContainerFormat.matroska;
                    result.Options.Audio.Codec = AudioCodecs.mp3;
                    switch (quality) {
                        case PresetQuality.ULTRA:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar192k;
                            result.Options.Audio.BitRate = AudioBitRate.abr320;
                            break;
                        case PresetQuality.HIGH:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar96k;
                            result.Options.Audio.BitRate = AudioBitRate.abr256;
                            break;
                        case PresetQuality.MEDIUM:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar48k;
                            result.Options.Audio.BitRate = AudioBitRate.abr192;
                            break;
                        case PresetQuality.LOW:
                            result.Options.Audio.SamplingRate = SamplingFrequency.ar44k;
                            result.Options.Audio.BitRate = AudioBitRate.abr128;
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
            var result = new Preset(PresetType.VIDEO);
            switch (target) {
                case PresetTarget.SIZE:
                    if(result.Options.Video == null) {
                        result.Options.Video = new VideoOptions();
                    }
                    result.Options.Format = ContainerFormat.webm;
                    result.Options.Video.Codec = VideoCodecs.vp8;
                    switch (quality) {
                        case PresetQuality.ULTRA:
                            result.Options.Video.Resolution = Resolutions.r2160p;
                            result.Options.Video.FPS = 60;
                            result.Options.Video.CalcBitRate();
                            break;
                        case PresetQuality.HIGH:
                            result.Options.Video.Resolution = Resolutions.r1080p;
                            result.Options.Video.FPS = 60;
                            result.Options.Video.CalcBitRate();
                            break;
                        case PresetQuality.MEDIUM:
                            result.Options.Video.Resolution = Resolutions.r1080p;
                            result.Options.Video.FPS = 30;
                            result.Options.Video.CalcBitRate();
                            break;
                        case PresetQuality.LOW:
                            result.Options.Video.Resolution = Resolutions.r720p;
                            result.Options.Video.FPS = 30;
                            result.Options.Video.CalcBitRate();
                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.QUALITY:
                    if (result.Options.Video == null) {
                        result.Options.Video = new VideoOptions();
                    }
                    result.Options.Format = ContainerFormat.matroska;
                    result.Options.Video.Codec = VideoCodecs.mpeg4;
                    switch (quality) {
                        case PresetQuality.ULTRA:
                            result.Options.Video.Resolution = Resolutions.r4320p;
                            result.Options.Video.FPS = 60;
                            result.Options.Video.CalcBitRate();
                            break;
                        case PresetQuality.HIGH:
                            result.Options.Video.Resolution = Resolutions.r2160p;
                            result.Options.Video.FPS = 60;
                            result.Options.Video.CalcBitRate();
                            break;
                        case PresetQuality.MEDIUM:
                            result.Options.Video.Resolution = Resolutions.r1080p;
                            result.Options.Video.FPS = 60;
                            result.Options.Video.CalcBitRate();
                            break;
                        case PresetQuality.LOW:
                            result.Options.Video.Resolution = Resolutions.r1080p;
                            result.Options.Video.FPS = 30;
                            result.Options.Video.CalcBitRate();
                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.SPEED:
                    if (result.Options.Video == null) {
                        result.Options.Video = new VideoOptions();
                    }
                    result.Options.Format = ContainerFormat.matroska;
                    result.Options.Video.Codec = VideoCodecs.hevc;
                    switch (quality) {
                        case PresetQuality.ULTRA:
                            result.Options.Video.Resolution = Resolutions.r2160p;
                            result.Options.Video.FPS = 60;
                            result.Options.Video.CalcBitRate();
                            break;
                        case PresetQuality.HIGH:
                            result.Options.Video.Resolution = Resolutions.r1080p;
                            result.Options.Video.FPS = 60;
                            result.Options.Video.CalcBitRate();
                            break;
                        case PresetQuality.MEDIUM:
                            result.Options.Video.Resolution = Resolutions.r1080p;
                            result.Options.Video.FPS = 30;
                            result.Options.Video.CalcBitRate();
                            break;
                        case PresetQuality.LOW:
                            result.Options.Video.Resolution = Resolutions.r720p;
                            result.Options.Video.FPS = 30;
                            result.Options.Video.CalcBitRate();
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
            var result = new Preset(PresetType.IMAGE);
            switch (target) {
                case PresetTarget.SIZE:
                    if (result.Options.Image == null) {
                        result.Options.Image = new ImageOptions();
                    }
                    result.Options.Format = null;
                    result.Options.Image.Format = ImageFormat.JPG;
                    switch (quality) {
                        case PresetQuality.ULTRA:
                            result.Options.Image.PixelFormat = PixelFormats.RGB24;
                            result.Options.Image.CompressionLevel = 1;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r4320p);
                            break;
                        case PresetQuality.HIGH:
                            result.Options.Image.PixelFormat = PixelFormats.RGB24;
                            result.Options.Image.CompressionLevel = 7;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r2160p);
                            break;
                        case PresetQuality.MEDIUM:
                            result.Options.Image.PixelFormat = PixelFormats.RGB24;
                            result.Options.Image.CompressionLevel = 10;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r1440p);
                            break;
                        case PresetQuality.LOW:
                            result.Options.Image.PixelFormat = PixelFormats.RGB24;
                            result.Options.Image.CompressionLevel = 15;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r1080p);
                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.QUALITY:
                    if (result.Options.Image == null) {
                        result.Options.Image = new ImageOptions();
                    }
                    result.Options.Format = null;
                    result.Options.Image.Format = ImageFormat.TGA;
                    switch (quality) {
                        case PresetQuality.ULTRA:
                            result.Options.Image.PixelFormat = PixelFormats.RGBA;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r4320p);
                            break;
                        case PresetQuality.HIGH:
                            result.Options.Image.PixelFormat = PixelFormats.RGBA;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r2160p);
                            break;
                        case PresetQuality.MEDIUM:
                            result.Options.Image.PixelFormat = PixelFormats.RGBA;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r1440p);
                            break;
                        case PresetQuality.LOW:
                            result.Options.Image.PixelFormat = PixelFormats.RGB24;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r1080p);
                            break;
                        default:
                            throw new ArgumentException("quality");
                    }
                    break;
                case PresetTarget.SPEED:
                    if (result.Options.Image == null) {
                        result.Options.Image = new ImageOptions();
                    }
                    result.Options.Format = null;
                    result.Options.Image.Format = ImageFormat.PNG;
                    switch (quality) {
                        case PresetQuality.ULTRA:
                            result.Options.Image.PixelFormat = PixelFormats.RGBA;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r4320p);
                            break;
                        case PresetQuality.HIGH:
                            result.Options.Image.PixelFormat = PixelFormats.RGBA;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r2160p);
                            break;
                        case PresetQuality.MEDIUM:
                            result.Options.Image.PixelFormat = PixelFormats.RGBA;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r1440p);
                            break;
                        case PresetQuality.LOW:
                            result.Options.Image.PixelFormat = PixelFormats.RGB24;
                            result.Options.Image.Size = EnumHelper.GetResolutionValue(Resolutions.r1080p);
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
