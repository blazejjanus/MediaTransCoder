using System.Text;

namespace MediaTransCoder.Backend {
    public class Measurer {
        public long InputSize { get; private set; }
        public long OutputSize { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Time { 
            get {
                return EndTime - StartTime;
            } 
        }
        public double CompressionRate {
            get {
                return (1 - (double)OutputSize / InputSize) * 100;
            }
        }
        private FileOption Files { get; set; }

        public Measurer(FileOption files) {
            Files = files;
        }

        public string GetStats() {
            var result = string.Empty;
            result = "Conversion statistics:\n";
            result += GetTimeStats("\t");
            result += GetSizeStats("\t");
            return result;
        }

        public string GetTimeStats(string prefix = "") {
            var sb = new StringBuilder();
            sb.Append(prefix + "Conversion Time: " + Time.ToString("g") + "\n");
            if(Time.TotalDays < 1) {
                sb.Append(prefix + "\t(" + StartTime.ToString("HH:mm:ss") + " - " + EndTime.ToString("HH:mm:ss") + ")\n");
            } else {
                sb.Append(prefix + "\t(" + StartTime.ToString("dd-mm-yyy HH:mm:ss") + " - " + EndTime.ToString("dd-mm-yyy HH:mm:ss") + ")\n");
            }
            return sb.ToString();
        }

        public string GetSizeStats(string prefix = "") {
            var sb = new StringBuilder();
            sb.Append(prefix + "Compression rate: " + CompressionRate.ToString("0.00") + "%\n");
            sb.Append(prefix + "(" + InputSize + " -> " + OutputSize + ")");
            return sb.ToString();
        }

        public void StartMeasure() {
            if (!File.Exists(Files.Input)) {
                throw new FileNotFoundException();
            }
            var input = new FileInfo(Files.Input);
            InputSize = input.Length;
            StartTime = DateTime.Now;
        }

        public void EndMeasure() {
            EndTime = DateTime.Now;
            if (!File.Exists(Files.Output)) {
                throw new FileNotFoundException();
            }
            var output = new FileInfo(Files.Output);
            OutputSize = output.Length;
        }
    }
}
