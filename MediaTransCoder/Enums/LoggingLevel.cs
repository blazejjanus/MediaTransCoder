using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MediaTransCoder.Backend {
    public enum LoggingLevel {
        [Display(Name = "quiet")]
        NONE = 0,       //No logging
        [Display(Name = "error")]
        ERROR = 1,      //Log errors only
        [Display(Name = "warning")]
        WARNING = 2,    //Log errors and warnings
        [Display(Name = "info")]
        INFO = 3        //Log all events
    }
}
