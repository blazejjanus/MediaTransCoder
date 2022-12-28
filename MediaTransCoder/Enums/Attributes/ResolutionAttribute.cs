using System.Numerics;

namespace MediaTransCoder.Backend {
    public class ResolutionAttribute : Attribute {
        public Vector2 Size { get; set; }
        public string? Name { get; set; }

        public ResolutionAttribute(uint wigth, uint height, string? name = null) { 
            Size = new Vector2(wigth, height);
            Name = name;
        }

        public string GetResolution() {
            return Size.X + ":" + Size.Y;
        }
    }
}
