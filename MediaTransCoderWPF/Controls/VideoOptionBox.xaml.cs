using MediaTransCoder.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediaTransCoder.WPF.Controls {
    /// <summary>
    /// Interaction logic for VideoOptionBox.xaml
    /// </summary>
    public partial class VideoOptionBox : UserControl, IRefreshableControl {
        public VideoOptions Video { get; set; }
        public ContainerFormat? SelectedFormat {
            get {
                return selectedFormat;
            }
            set {
                selectedFormat = value;
                Refresh();
            }
        }
        private ContainerFormat? selectedFormat;
        private WPFContext context = WPFContext.Get();

        public VideoOptionBox() {
            InitializeComponent();
            Video = new VideoOptions();
            PreFillForm();
        }

        public void Refresh() {
            vcodecInput.Items.Clear();
            List<VideoCodecs> vcodecs = new List<VideoCodecs>();
            if (SelectedFormat != null) {
                vcodecs = Compatibility.GetCompatibleVideoCodecs(SelectedFormat.Value);
            } else {
                vcodecs = Enum.GetValues(typeof(VideoCodecs)).Cast<VideoCodecs>().ToList();
            }
            if (selectedFormat != null) {
                var codec = Compatibility.GetDefaultVideoCodec(selectedFormat.Value);
                if(codec != null) {
                    Video.Codec = codec.Value;
                }
            }
            foreach (var vcodec in vcodecs) {
                if (!vcodecInput.Items.Contains(vcodec.ToString())) {
                    vcodecInput.Items.Add(vcodec.ToString());
                }
            }
            if (vcodecInput.Items.Contains(Video.Codec.ToString())) {
                vcodecInput.SelectedIndex = vcodecInput.Items.IndexOf(Video.Codec.ToString());
            }
        }

        private void PreFillForm() {
            //Video codec
            List<VideoCodecs> vcodecs = new List<VideoCodecs>();
            if (SelectedFormat != null) {
                vcodecs = Compatibility.GetCompatibleVideoCodecs(SelectedFormat.Value);
            } else {
                vcodecs = Enum.GetValues(typeof(VideoCodecs)).Cast<VideoCodecs>().ToList();
            }
            foreach (var vcodec in vcodecs) {
                if (vcodecInput.Items.Contains(vcodec.ToString())) {
                    vcodecInput.Items.Add(vcodec.ToString());
                }
            }
            if (vcodecInput.Items.Contains(Video.Codec.ToString())) {
                vcodecInput.SelectedIndex = vcodecInput.Items.IndexOf(Video.Codec.ToString());
            }
            //Resolution selection
            var resolutions = Enum.GetValues(typeof(Resolutions));
            foreach (Resolutions resolution in resolutions) {
                if(!resolutionInput.Items.Contains(EnumHelper.GetName(resolution))) {
                    resolutionInput.Items.Add(EnumHelper.GetName(resolution));
                }
            }
            if (resolutionInput.Items.Contains(EnumHelper.GetName(Video.Resolution))) {
                resolutionInput.SelectedIndex = resolutionInput.Items.IndexOf(EnumHelper.GetName(Video.Resolution));
            }
            //FPS Selection
            fpsInput.Text = Video.FPS.ToString();
            fpsInput.ToolTip = "20 - 120";
            //BitRate Selections
            Video.CalcBitRate();
            brInput.Text = Video.BitRate.ToString();
        }

        private void vcodecInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var vcodecs = Enum.GetValues(typeof(VideoCodecs));
            foreach (VideoCodecs vcodec in vcodecs) {
                if(vcodecInput.SelectedItem == null) {
                    break;
                }
                if(vcodec.ToString() == vcodecInput.SelectedItem.ToString()) {
                    Video.Codec = vcodec;
                    break;
                }
            }
        }

        private void resolutionInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var resolutions = Enum.GetValues(typeof(Resolutions));
            foreach (Resolutions resolution in resolutions) {
                if(EnumHelper.GetName(resolution) == resolutionInput.SelectedItem.ToString()) {
                    Video.Resolution = resolution;
                    Video.CalcBitRate();
                    brInput.Text = Video.BitRate.ToString();
                    break;
                }
            }
        }

        private void fpsInput_TextChanged(object sender, TextChangedEventArgs e) {
            string input = fpsInput.Text.Trim();
            int val = 0;
            if (int.TryParse(input, out val)) {
                if(val < 20) {
                    context.Display?.Send("Minimalna wartość FPS to 20!", MessageType.ERROR);
                    fpsInput.Text = Video.FPS.ToString();
                }
                if(val > 120) {
                    context.Display?.Send("Maksymalna wartość FPS to 120!", MessageType.ERROR);
                    fpsInput.Text = Video.FPS.ToString();
                }
                Video.FPS = val;
                Video.CalcBitRate();
                brInput.Text = Video.BitRate.ToString();
            } else {
                fpsInput.Text = Video.FPS.ToString();
            }
        }

        private void brInput_TextChanged(object sender, TextChangedEventArgs e) {
            string input = fpsInput.Text.Trim();
            int val = 0;
            if (int.TryParse(input, out val)) {
                Video.BitRate = val;
            } else {
                brInput.Text = Video.BitRate.ToString();
            }
        }

        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
