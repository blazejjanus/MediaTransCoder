using MediaTransCoder.Backend.Compatibility;

namespace MediaTransCoder.Backend
{
    public class TestEndpoint : Endpoint
    {
        public TestEndpoint(BackendConfig config, IDisplay gui) : base(config, gui)
        {

        }
        public List<CompatibilityChart> TestAudio(string input, string? output = null) {
            return CompatibilityTest.Audio(input, output);
        }
        public List<CompatibilityChart> TestVideo(string input, string? output = null) {
            return CompatibilityTest.Video(input, output);
        }
        public List<CompatibilityChart> TestAudioVideo(string input, string? output = null) {
            return CompatibilityTest.AudioVideo(input, output);
        }
    }
}
