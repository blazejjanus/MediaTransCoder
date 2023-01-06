using System.ComponentModel.DataAnnotations;

namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported audio codecs. Use Name to get command, use Command to get dispalyed name.
    /// </summary>
    public enum AudioCodecs {
        [AudioCodec("libvorbis", "vorbis")]
        libvorbis,
        [AudioCodec("libopus", "opus")]
        libopus,
        [AudioCodec("mp3", "mp3", ".mp3")]
        mp3,
        [AudioCodec("aac", "aac", ".aac")]
        aac,
        [AudioCodec("wmav1", "wmav1", ".wma")]
        wmav1,
        [AudioCodec("wmav2", "wmav2", ".wma")]
        wmav2,
        [AudioCodec("ac3", "ac3", ".ac3")]
        ac3,
        [AudioCodec("eac3", "eac3", ".eac3")]
        eac3,
        [AudioCodec("flac", "flac", ".flac")]
        flac,
        [AudioCodec("pcm_s16le", "PCM")]
        pcm
    }
}
