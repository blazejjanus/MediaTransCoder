using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported container formats. Use Name to get command, use Description to get dispalyed name.
    /// </summary>
    public enum ContainerFormat {
        [Display(Name = "webm", Description = "Webm")]
        webm,
        [Display(Name = "wav", Description = "Wave")]
        wav,
        [Display(Name = "3gp", Description = "3gp")]
        c3gp,
        [Display(Name = "flv", Description = "FlashVideo")]
        flv,
        [Display(Name = "matroska", Description = "Matroska")]
        matroska,
        [Display(Name = "ogg", Description = "OGG")]
        ogg,
        [Display(Name = "avi", Description = "AVI")]
        avi
    }
}
