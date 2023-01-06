using System.Numerics;

namespace MediaTransCoder.Backend {
    public class ResolutionAttribute : Attribute {
        /// <summary>
        /// Vector representing resolution in px
        /// </summary>
        public Vector2 Size { get; set; }
        /// <summary>
        /// Displayed resolution name if there is such a name (e.g. 4K UHD)
        /// </summary>
        public string? Name { get; set; }

        public ResolutionAttribute(uint wigth, uint height, string? name = null) { 
            Size = new Vector2(wigth, height);
            Name = name;
        }

        /// <summary>
        /// Returns Ffmpeg formated resolution value
        /// </summary>
        /// <returns></returns>
        public string GetResolution() {
            return Size.X + ":" + Size.Y;
        }
    }
}
