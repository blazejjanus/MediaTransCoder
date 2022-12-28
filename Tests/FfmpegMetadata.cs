using MediaTransCoder.Backend;
using System.Diagnostics;

namespace MediaTransCoder.Tests {
    [TestClass]
    public class FfmpegMetadata {
        [TestMethod]
        public void ReadTest() {
            using(var info = new Backend.FfmpegMetadata()) {
                Assert.IsTrue(File.Exists(UnitTestsEnvironment.InputVideo));
                info.Read(UnitTestsEnvironment.InputVideo);
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