using System.Diagnostics;

namespace MediaTransCoder.Tests {
    [TestClass]
    public class FfmpegMetadataTests {
        [TestMethod]
        public void ReadVideoMetadata() {
            var env = UnitTestsEnvironment.Get();
            using (var info = new Backend.FfmpegMetadata()) {
                Assert.IsTrue(File.Exists(env.Video.InputFile));
                info.Read(env.Video.InputFile);
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
            var env = UnitTestsEnvironment.Get();
            using (var info = new Backend.FfmpegMetadata()) {
                Assert.IsTrue(File.Exists(env.Audio.InputFile));
                info.Read(env.Audio.InputFile);
                Assert.IsNotNull(info.TotalNumberOfFrames);
                Assert.IsNotNull(info.FPS);
                Debug.WriteLine("NoF: " + info.TotalNumberOfFrames);
                Debug.WriteLine("FPS: " + info.FPS);
                Assert.IsTrue(info.TotalNumberOfFrames > 0);
                Assert.IsTrue(info.FPS > 0);
            }
        }
    }
}