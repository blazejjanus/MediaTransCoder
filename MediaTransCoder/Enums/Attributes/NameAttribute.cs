namespace MediaTransCoder.Backend {
    public class NameAttribute : Attribute {
        public string Name { get; set; }
        public string DisplayedName { get; set; }

        public NameAttribute(string name) {
            Name = name;
            DisplayedName = name;
        }

        public NameAttribute(string name, string displayedName) {
            Name = name;
            DisplayedName = displayedName;
        }
    }
}
