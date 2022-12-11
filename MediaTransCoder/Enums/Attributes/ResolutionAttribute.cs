using System.Numerics;

namespace MediaTransCoder.Backend
{
    public class ResolutionAttribute : Attribute
    {
        public ResolutionAttribute(uint wigth, uint height, string? name = null) { 
            Width = wigth;
            Height = height;
            Name = name;
        }
        public string GetResolution() {
            return Width + ":" + Height;
        }
        public Vector2 GetVector() {
            return new Vector2(Width, Height);
        }
        public uint Height;
        public uint Width;
        public string? Name;
    }
}
