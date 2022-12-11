using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MediaTransCoder.Backend {
    public enum LoggingLevel {
        [Display(Name = "quiet")]
        NONE,       //No logging
        [Display(Name = "error")]
        ERROR,      //Log errors only
        [Display(Name = "warning")]
        WARNING,    //Log errors and warnings
        [Display(Name = "info")]
        INFO        //Log all events
    }
}
