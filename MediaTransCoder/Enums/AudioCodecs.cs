using System.ComponentModel.DataAnnotations;

namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported audio codecs. Use Name to get command, use Description to get dispalyed name.
    /// </summary>
    public enum AudioCodecs {
        [VideoCodec("libvorbis", "vorbis", ".ogg")]
        libvorbis,
        [VideoCodec("libopus", "opus", ".ogg")]
        libopus,
        [VideoCodec("mp3", "mp3", ".mp3")]
        mp3,
        [VideoCodec("aac", "aac", ".aac")]
        aac,
        [VideoCodec("wmav1", "wmav1", ".wma")]
        wmav1,
        [VideoCodec("wmav2", "wmav2", ".wma")]
        wmav2,
        [VideoCodec("ac3", "ac3", ".ac3")]
        ac3,
        [VideoCodec("eac3", "eac3", ".eac3")]
        eac3,
        [VideoCodec("flac", "flac", ".flac")]
        flac,
        [VideoCodec("alac", "alac", ".alac")]
        alac
    }
}
