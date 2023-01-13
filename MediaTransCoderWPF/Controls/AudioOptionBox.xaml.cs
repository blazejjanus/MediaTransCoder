using MediaTransCoder.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediaTransCoder.WPF.Controls {
    /// <summary>
    /// Interaction logic for AudioOptionBox.xaml
    /// </summary>
    public partial class AudioOptionBox : UserControl, IRefreshableControl {
        public AudioOptions Audio { get; set; }
        public ContainerFormat? SelectedFormat {
            get {
                return selectedFormat;
            }
            set {
                selectedFormat = value;
                PreFillForm();
            }
        }
        private ContainerFormat? selectedFormat;
        private WPFContext context = WPFContext.Get();

        public AudioOptionBox() {
            InitializeComponent();
            Audio = new AudioOptions();
            PreFillForm();
        }

        public void Refresh() {
            acodecInput.Items.Clear();
            //Acodec
            List<AudioCodecs> acodecs = new List<AudioCodecs>();
            if (SelectedFormat != null) {
                acodecs = Compatibility.GetCompatibleAudioCodecs(SelectedFormat.Value);
            } else {
                acodecs = Enum.GetValues(typeof(AudioCodecs)).Cast<AudioCodecs>().ToList();
            }
            if (selectedFormat != null) {
                Audio.Codec = Compatibility.GetDefaultAudioCodec(selectedFormat.Value);
            }
            foreach (var acodec in acodecs) {
                if (!acodecInput.Items.Contains(acodec.ToString())) {
                    acodecInput.Items.Add(acodec.ToString());
                }
            }
            if (acodecInput.Items.Contains(Audio.Codec.ToString())) {
                acodecInput.SelectedIndex = acodecInput.Items.IndexOf(Audio.Codec.ToString());
            }
        }

        public void PreFillForm() {
            //Acodec
            List<AudioCodecs> acodecs = new List<AudioCodecs>();
            if(SelectedFormat != null) {
                acodecs = Compatibility.GetCompatibleAudioCodecs(SelectedFormat.Value);
            } else {
                acodecs = Enum.GetValues(typeof(AudioCodecs)).Cast<AudioCodecs>().ToList();
            }
            foreach (var acodec in acodecs) {
                if (!acodecInput.Items.Contains(acodec.ToString())) {
                    acodecInput.Items.Add(acodec.ToString());
                }
            }
            if (acodecInput.Items.Contains(Audio.Codec.ToString())) {
                acodecInput.SelectedIndex = acodecInput.Items.IndexOf(Audio.Codec.ToString());
            }
            //BitRate
            var brs = Enum.GetValues(typeof(AudioBitRate));
            foreach (AudioBitRate br in brs) {
                if(!brInput.Items.Contains(EnumHelper.GetName(br))) {
                    brInput.Items.Add(EnumHelper.GetName(br));
                }
            }
            if (brInput.Items.Contains(EnumHelper.GetName(Audio.BitRate))) {
                brInput.SelectedIndex = brInput.Items.IndexOf(EnumHelper.GetName(Audio.BitRate));
            }
            //AudioChannels
            acInput.Value = Audio.AudioChannels;
            acInput.MinValue = 2;
            acInput.MaxValue = 255;
            acInput.ToolTip = "2 - 255";
            //SamplingRate
            var srs = Enum.GetValues(typeof(SamplingFrequency));
            foreach (SamplingFrequency sr in srs) {
                if (!srInput.Items.Contains(EnumHelper.GetName(sr))) {
                    srInput.Items.Add(EnumHelper.GetName(sr));
                }
            }
            if (srInput.Items.Contains(EnumHelper.GetName(Audio.SamplingRate))) {
                srInput.SelectedIndex = srInput.Items.IndexOf(EnumHelper.GetName(Audio.SamplingRate));
            }
        }

        private void acodecInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var acodecs = Enum.GetValues(typeof(AudioCodecs));
            foreach (AudioCodecs acodec in acodecs) {
                if(acodecInput.SelectedItem == null) {
                    break;
                }
                if (acodec.ToString() == acodecInput.SelectedItem.ToString()) {
                    Audio.Codec = acodec;
                    break;
                }
            }
        }

        private void srInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var srs = Enum.GetValues(typeof(SamplingFrequency));
            foreach (SamplingFrequency sr in srs) {
                if (EnumHelper.GetName(sr) == srInput.SelectedItem.ToString()) {
                    Audio.SamplingRate = sr;
                    break;
                }
            }
        }

        private void brInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var brs = Enum.GetValues(typeof(AudioBitRate));
            foreach (AudioBitRate br in brs) {
                if (EnumHelper.GetName(br) == brInput.SelectedItem.ToString()) {
                    Audio.BitRate = br;
                    break;
                }
            }
        }

        private void acInput_ValueChanged(object sender, EventArgs e) {
            Audio.AudioChannels = acInput.Value;
        }
    }
}
