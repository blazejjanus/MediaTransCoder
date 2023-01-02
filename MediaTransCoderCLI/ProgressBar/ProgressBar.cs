namespace MediaTransCoder.CLI {
    public class ProgressBar {
        private ProgressBarOptions Options { get; set; }
        private (int Left, int Top) Location { get; set; }
        public int Size { get; private set; }
        public int Done { get; private set; }
        private int StartingIndex { get; set; }
        public double Percentage {
            get {
                return Math.Round((double)Done / Size * 100, 2);
            }
        }

        public ProgressBar(ProgressBarOptions options, int size = 100) {
            Options = options;
            Location = Console.GetCursorPosition();
            Size = size;
            Done = 0;
            StartingIndex = 0;
        }

        public ProgressBar(int size = 100) {
            Options = new ProgressBarOptions();
            Location = Console.GetCursorPosition();
            Size = size;
            Done = 0;
            StartingIndex = 0;
        }

        public void Draw() {
            if (Options.DrawInNewLine) {
                Console.WriteLine();
            }
            Console.SetCursorPosition(Location.Left, Location.Top);
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = Options.UndoneColor;
            if (Options.Prefix != null) {
                Console.Write(Options.Prefix + " ");
                StartingIndex += Options.Prefix.Length + 1;
            }
            if (Options.StartingChar != null) {
                Console.Write(Options.StartingChar);
                StartingIndex += 1;
            }
            for (int i = 0; i < Size; i++) {
                Console.Write(Options.UndoneChar);
            }
            Console.Write(Options.EndingChar);
            Console.ForegroundColor = defaultColor;
            if (Options.EndWithNewLine) {
                Console.WriteLine();
            }
        }

        public void Update(int progress, bool writeValue = false) {
            var originalLocation = Console.GetCursorPosition();
            Done = progress;
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = Options.DoneColor;
            Console.SetCursorPosition(Location.Left + StartingIndex, Location.Top);
            for (int i = 0; i < Done; i++) {
                Console.Write(Options.DoneChar);
            }
            Console.SetCursorPosition(Location.Left + StartingIndex + Size + 1, Location.Top);
            if (writeValue) {
                Console.Write(" " + progress + "%");
            } else {
                Console.Write(" " + Percentage + "%");
            }
            Console.ForegroundColor = defaultColor;
            Console.SetCursorPosition(originalLocation.Left, originalLocation.Top);
        }

        public void Update(double progress, bool writeValue = false) {
            if (progress > 1) {
                progress = Math.Round(progress / 100, 2);
            }
            var originalLocation = Console.GetCursorPosition();
            Done = (int)Math.Round(progress * Size);
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = Options.DoneColor;
            Console.SetCursorPosition(Location.Left + StartingIndex, Location.Top);
            for (int i = 0; i < Done; i++) {
                Console.Write(Options.DoneChar);
            }
            Console.SetCursorPosition(Location.Left + StartingIndex + Size + 1, Location.Top);
            if (writeValue) {
                Console.Write(" " + progress + "%");
            } else {
                Console.Write(" " + Percentage + "%");
            }
            Console.ForegroundColor = defaultColor;
            Console.SetCursorPosition(originalLocation.Left, originalLocation.Top);
        }
    }
}
