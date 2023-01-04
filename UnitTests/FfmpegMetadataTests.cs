using System.Diagnostics;

namespace MediaTransCoder.UnitTests {
    [TestClass]
    public class FfmpegMetadataTests {
        TestingEnvironment env = TestingEnvironment.Get();
        public FfmpegMetadataTests() {
            Mocker.MockContext();
        }

        [TestMethod]
        public void ReadVideoMetadata() {
            using (var info = new Backend.FfmpegMetadata()) {
                Assert.IsTrue(File.Exists(env.Video.Input));
                info.ReadVideo(env.Video.Input);
                Assert.IsNotNull(info.TotalNumberOfFrames);
                Assert.IsNotNull(info.FPS);
                Debug.WriteLine("NoF: " + info.TotalNumberOfFrames);
                Debug.WriteLine("FPS: " + info.FPS);
                Assert.IsTrue(info.TotalNumberOfFrames > 0);
                Assert.IsTrue(info.FPS > 0);
            }
        }

        [TestMethod]
        public void ReadAudioMetadata() {
            using (var info = new Backend.FfmpegMetadata()) {
                Assert.IsTrue(File.Exists(env.Audio.Input));
                info.ReadAudio(env.Audio.Input);
                Assert.IsNotNull(info.TotalNumberOfFrames);
                Assert.IsNotNull(info.FPS);
                Debug.WriteLine("NoF: " + info.TotalNumberOfFrames);
                Debug.WriteLine("FPS: " + info.FPS);
                //Assert.IsTrue(info.TotalNumberOfFrames > 0);
                //Assert.IsTrue(info.FPS > 0);
            }
        }
    }
}