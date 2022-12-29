namespace MediaTransCoder.Backend {
    internal class TimeParser {
        internal int TotalSeconds { 
            get {
                return (int)Time.TotalSeconds;
            } 
        }

        internal int TotalMiliseconds {
            get {
                return (int)Time.TotalMilliseconds;
            }
        }

        internal TimeSpan Time { get; private set; }

        internal TimeParser(string time) {
            Time = Parse(time);
        }

        public override string ToString() {
            return Time.ToString("HH:mm:ss");
        }

        private TimeSpan Parse(string timeString) {
            TimeSpan time;
            if (TimeSpan.TryParse(timeString, out time)) {
                return time;
            } else {
                throw new Exception("Unabel to parse time: " + timeString);
            }
        }
    }
}
