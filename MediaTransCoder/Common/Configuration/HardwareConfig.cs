namespace MediaTransCoder.Backend {
    public class HardwareConfig {
        /// <summary>
        /// Number of logical cores ffmpeg will use
        /// </summary>
        public int CPUCores { get; set; }
        /// <summary>
        /// Number of Logical Cores of the installed CPU
        /// </summary>
        public int MaxCPUCores { get; set; }
        /// <summary>
        /// Inforamtion about installed GPU
        /// </summary>
        public GPUType GPU { get; set; }

        public HardwareConfig() { 
            CPUCores = 0; //Value of optimal Threads number for ffmpeg
            GPU = GPUType.NONE; //No GPU acceleration
        }
    }
}
