using System.Globalization;

namespace MediaTransCoder.Backend {
    internal static class NumberParser {
        internal static int ParseDoubleStringToInt(string number) {
            number = number.Trim();
            decimal value;
            if (decimal.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture, out value)) {
                return (int)Math.Round(value);
            } else {
                throw new Exception("Unable to parse to int: " + number);
            }
        }
    }
}
