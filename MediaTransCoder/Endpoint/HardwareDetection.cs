using System.Management;
using System.Runtime.InteropServices;

namespace MediaTransCoder.Backend {
    public static class HardwareDetection {
        public static HardwareConfig GetConfig() {
            var result = new HardwareConfig();
            result.CPUCores = Environment.ProcessorCount;
            result.GPU = GPUType.NONE;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                ManagementObject? controller = searcher.Get().Cast<ManagementObject>().FirstOrDefault();
                if (controller != null) {
                    string gpuName = controller["Name"]?.ToString()?.ToLower() ?? string.Empty;
                    if (gpuName.Contains("amd")) {
                        result.GPU = GPUType.AMD;
                    }
                    if (gpuName.Contains("nvidia")) {
                        result.GPU = GPUType.NVIDIA;
                    }
                }
            }
            return result;
        }
    }
}
