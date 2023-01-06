﻿using System.Text;

namespace MediaTransCoder.Backend {
    //TODO: Get propper values by codec
    /// <summary>
    /// Options used for audio conversion.
    /// </summary>
    public class AudioOptions {
        /// <summary>
        /// Audio codec used in conversion.
        /// </summary>
        public AudioCodecs Codec { get; set; }
        /// <summary>
        /// Audio BitRate
        /// </summary>
        public uint BitRate {
            get {
                return br;
            }
            set {
                if(value < 6 || value > 640) {
                    throw new ArgumentOutOfRangeException("BitRate");
                }
                br = value;
            }
        }
        /// <summary>
        /// Number of audio channels
        /// </summary>
        public uint AudioChannels {
            get {
                return ac;
            }
            set {
                if(value < 1 || value > 255) {
                    throw new ArgumentOutOfRangeException("AudioChannels");
                }
                ac = value;
            }
        }
        /// <summary>
        /// Audio sampling frequency
        /// </summary>
        public uint SamplingRate {
            get {
                return ar;
            }
            set {
                if(value < 8000 || value > 192000) {
                    throw new ArgumentOutOfRangeException("SamplingRate");
                }
                ar = value;
            }
        }

        private uint ac;
        private uint br;
        private uint ar;

        public AudioOptions() {
            Codec = AudioCodecs.mp3;
            BitRate = 192;
            AudioChannels = 2;
        }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine("Audio:");
            sb.AppendLine("\tCodec:      " + Codec);
            sb.AppendLine("\tBitRate:    " + BitRate);
            sb.AppendLine("\tSampling:   " + SamplingRate);
            sb.AppendLine("\tChannels:   " + AudioChannels);
            return sb.ToString();
        }
    }
}
