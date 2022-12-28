namespace MediaTransCoder.Backend {
    public class ContainerFormatAttribute : Attribute {
        public string Name { get; set; }
        public string Description { get; set; }

        public ContainerFormatAttribute(string name, string description) {
            Name = name;
            Description = description;
        }

        public ContainerFormatAttribute(string name) {
            Name = name;
            Description = name;
        }
    }
}
