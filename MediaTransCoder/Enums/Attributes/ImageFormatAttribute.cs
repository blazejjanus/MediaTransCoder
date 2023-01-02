namespace MediaTransCoder.Backend {
    public class ImageFormatAttribute : Attribute {
        public string Name { get; set; }
        public List<string> Extensions { get; set; }

        public ImageFormatAttribute(string name) {
            Name = name;
            Extensions = new List<string>();
            Extensions.Add("." + name);
        }

        public ImageFormatAttribute(string name, string[] extensions) { 
            Name = name;
            Extensions = extensions.ToList();
        }
    }
}
