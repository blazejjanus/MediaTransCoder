namespace MediaTransCoder.CLI {
    public class ProgressBarOptions {
        public char DoneChar { get; set; } = '#';
        public char UndoneChar { get; set; } = ' ';
        public char? StartingChar { get; set; } = '[';
        public char? EndingChar { get; set; } = ']';
        public string? Prefix { get; set; } = null;
        public ConsoleColor DoneColor { get; set; } = ConsoleColor.Green;
        public ConsoleColor UndoneColor { get; set; } = ConsoleColor.Gray;
        public bool DrawInNewLine { get; set; } = true;
        public bool EndWithNewLine { get; set; } = true;

        public ProgressBarOptions() {}
    }
}
