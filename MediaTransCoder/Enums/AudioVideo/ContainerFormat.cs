﻿namespace MediaTransCoder.Backend {
    /// <summary>
    /// Supported container formats. Use Name to get command, use Command to get displayed name.
    /// </summary>
    public enum ContainerFormat {
        [ContainerFormat("webm", "WebM", ".webm", null)]
        webm,
        [ContainerFormat("wav", "wave", null, ".wav")]
        wav,
        [ContainerFormat("3gp", "3GP", ".3gp", ".3gp")]
        c3gp,
        [ContainerFormat("flv", "FlashVideo", ".flv", null)]
        flv,
        [ContainerFormat("matroska", "Matroska", ".mkv", ".mka")]
        matroska,
        [ContainerFormat("ogg", "ogg", null, ".ogg")]
        ogg,
        [ContainerFormat("avi", "AVI", ".avi", ".avi")]
        avi,
        [ContainerFormat("asf", "ASF", ".asf", ".asf")]
        asf,
    }
}
