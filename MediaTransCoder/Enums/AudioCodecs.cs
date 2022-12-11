using System.ComponentModel.DataAnnotations;

namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported audio codecs. Use Name to get command, use Description to get dispalyed name.
    /// </summary>
    public enum AudioCodecs {
        [Display(Name = "libvorbis", Description = "Vorbis")]
        libvorbis,
        [Display(Name = "libopus", Description = "Opus")]
        libopus,
        [Display(Name = "mp3", Description = "MP3")]
        mp3,
        [Display(Name = "aac", Description = "AAC")]
        aac,
        [Display(Name = "wmav1", Description = "WMAv1")]
        wmav1,
        [Display(Name = "wmav2", Description = "WMAv2")]
        wmav2,
        [Display(Name = "ac3", Description = "AC3")]
        ac3,
        [Display(Name = "eac3", Description = "EAC3")]
        eac3,
        [Display(Name = "flac", Description = "FLAC")]
        flac,
        [Display(Name = "alac", Description = "ALAC")]
        alac
    }
}
