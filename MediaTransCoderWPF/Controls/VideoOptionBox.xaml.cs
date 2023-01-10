using MediaTransCoder.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for VideoOptionBox.xaml
    /// </summary>
    public partial class VideoOptionBox : UserControl {
        public VideoOptions Video { get; set; }
        private WPFContext context = WPFContext.Get();
        public VideoOptionBox() {
            InitializeComponent();
            Video = new VideoOptions();
            PreFillForm();
        }

        private void PreFillForm() {
            //Vcodec selection
            var vcodecs = Enum.GetValues(typeof(ContainerFormats));
            foreach (var vcodec in vcodecs) {
                vcodecInput.Items.Add(vcodec.ToString());
            }
            if (vcodecInput.Items.Contains(Video.Codec.ToString())) {
                vcodecInput.SelectedIndex = vcodecInput.Items.IndexOf(Video.Codec.ToString());
            }
            //Resolution selection
            var resolutions = Enum.GetValues(typeof(Resolutions));
            foreach (Resolutions resolution in resolutions) {
                resolutionInput.Items.Add(EnumHelper.GetName(resolution));
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
            var vcodecs = Enum.GetValues(typeof(ContainerFormats));
            foreach (ContainerFormats vcodec in vcodecs) {
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
