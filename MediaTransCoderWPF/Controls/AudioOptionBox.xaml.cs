using MediaTransCoder.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaTransCoder.WPF.Controls {
    /// <summary>
    /// Interaction logic for AudioOptionBox.xaml
    /// </summary>
    public partial class AudioOptionBox : UserControl {
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

        private void PreFillForm() {
            //Acodec
            List<AudioCodecs> acodecs = new List<AudioCodecs>();
            if(SelectedFormat != null) {
                acodecs = Compatibility.GetCompatibleAudioCodecs(SelectedFormat.Value);
            } else {
                acodecs = Enum.GetValues(typeof(AudioCodecs)).Cast<AudioCodecs>().ToList();
            }
            foreach (var acodec in acodecs) {
                acodecInput.Items.Add(acodec.ToString());
            }
            if (acodecInput.Items.Contains(Audio.Codec.ToString())) {
                acodecInput.SelectedIndex = acodecInput.Items.IndexOf(Audio.Codec.ToString());
            }
            //BitRate
            var brs = Enum.GetValues(typeof(AudioBitRate));
            foreach (AudioBitRate br in brs) {
                brInput.Items.Add(EnumHelper.GetName(br));
            }
            if (brInput.Items.Contains(EnumHelper.GetName(Audio.BitRate))) {
                brInput.SelectedIndex = brInput.Items.IndexOf(EnumHelper.GetName(Audio.BitRate));
            }
            //AudioChannels
            acInput.Text = Audio.AudioChannels.ToString();
            acInput.ToolTip = "2 - 255";
            //SamplingRate
            var srs = Enum.GetValues(typeof(SamplingFrequency));
            foreach (SamplingFrequency sr in srs) {
                srInput.Items.Add(EnumHelper.GetName(sr));
            }
            if (srInput.Items.Contains(EnumHelper.GetName(Audio.SamplingRate))) {
                srInput.SelectedIndex = srInput.Items.IndexOf(EnumHelper.GetName(Audio.SamplingRate));
            }
        }

        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void acodecInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var acodecs = Enum.GetValues(typeof(AudioCodecs));
            foreach (AudioCodecs acodec in acodecs) {
                if (acodec.ToString() == acodecInput.SelectedItem.ToString()) {
                    Audio.Codec = acodec;
                    break;
                }
            }
        }

        private void acInput_TextChanged(object sender, TextChangedEventArgs e) {
            string input = acInput.Text.Trim();
            int val = 0;
            if (int.TryParse(input, out val)) {
                if (val < 2) {
                    context.Display?.Send("Minimalna wartość to 2!", MessageType.ERROR);
                    acInput.Text = Audio.AudioChannels.ToString();
                }
                if (val > 255) {
                    context.Display?.Send("Maksymalna wartość to 255!", MessageType.ERROR);
                    acInput.Text = Audio.AudioChannels.ToString();
                }
                Audio.AudioChannels = val;
            } else {
                acInput.Text = Audio.AudioChannels.ToString();
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
    }
}
