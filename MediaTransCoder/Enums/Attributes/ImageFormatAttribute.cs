namespace MediaTransCoder.Backend {
    public class ImageFormatAttribute : Attribute {
        public string Name { get; set; }
        public string Command { get; set; }
        public List<string> Extensions { get; set; }

        public ImageFormatAttribute(string name) {
            Name = name;
            Command = name;
            Extensions = new List<string>();
            Extensions.Add("." + name);
        }

        public ImageFormatAttribute(string name, string command) {
            Name = name; 
            Command = command;
            Extensions = new List<string>();
            Extensions.Add("." + name);
        }

        public ImageFormatAttribute(string name, string[] extensions) { 
            Name = name;
            Command = name;
            Extensions = extensions.ToList();
        }

        public ImageFormatAttribute(string name, string command, string[] extensions) {
            Name = name;
            Command = command;
            Extensions = extensions.ToList();
        }
    }
}
