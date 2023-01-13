using MediaTransCoder.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

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

        public void PreFillForm() {
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
            fpsInput.MinValue = 20;
            fpsInput.MaxValue = 120;
            fpsInput.Value = Video.FPS;
            fpsInput.ToolTip = "20 - 120";
            //BitRate Selections
            Video.CalcBitRate();
            brInput.Increment = 10;
            brInput.MinValue = 100;
            brInput.MaxValue = 10000;
            brInput.Value = Video.BitRate;
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
                    brInput.Value = Video.BitRate;
                    break;
                }
            }
        }

        private void fpsInput_ValueChanged(object sender, EventArgs e) {
            Video.FPS = fpsInput.Value;
        }

        private void brInput_ValueChanged(object sender, EventArgs e) {
            Video.BitRate = brInput.Value;
        }
    }
}
