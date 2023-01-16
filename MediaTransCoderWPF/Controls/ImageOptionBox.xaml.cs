using MediaTransCoder.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows.Controls;

namespace MediaTransCoder.WPF.Controls {
    /// <summary>
    /// Interaction logic for ImageOptionBox.xaml
    /// </summary>
    public partial class ImageOptionBox : UserControl, IRefreshableControl {
        public ImageOptions Image {
            get {
                return new ImageOptions {
                    Format = format,
                    PixelFormat = pixelFormat,
                    Size = resControl.Resolution,
                    CompressionLevel = compressionLevel,
                    Effect = null,
                    Brightness = brightnessBar.Value,
                    Contrast = contrastBar.Value,
                    Saturation = saturationBar.Value
                };
            }
            set {
                if (value == null) {
                    throw new NullReferenceException();
                }
                format = value.Format;
                pixelFormat = value.PixelFormat;
                compressionLevel = value.CompressionLevel;
                resControl.Resolution = value.Size;
                brightnessBar.Value = value.Brightness;
                contrastBar.Value = value.Contrast;
                saturationBar.Value = value.Saturation;
            }
        }
        private ImageFormat format;
        private PixelFormats pixelFormat;
        private int? compressionLevel;

        public ImageOptionBox() {
            InitializeComponent();
            PreFillForm();
        }

        public void Refresh() {
            if(format == ImageFormat.JPG || format == ImageFormat.JPG2000) {
                jcrInput.IsEnabled = true;
                compressionLevel = 15;
                jcrInput.Value = 15;
                jcrInput.ToolTip = "Zakres wartości 1 - 31";
            } else {
                jcrInput.IsEnabled = false;
                compressionLevel = null;
                jcrInput.ToolTip = "Tylko dla formatów z rodziny JPEG.";
            }
        }

        public void PreFillForm() {
            //Image format
            List<ImageFormat> formats = Enum.GetValues(typeof(ImageFormat)).Cast<ImageFormat>().ToList();
            foreach(ImageFormat format in formats) {
                formatInput.Items.Add(EnumHelper.GetName(format));
            }
            if (formatInput.Items.Contains(EnumHelper.GetName(ImageFormat.JPG))) {
                formatInput.SelectedIndex = formatInput.Items.IndexOf(EnumHelper.GetName(ImageFormat.JPG));
                format = ImageFormat.JPG;
            }
            //Pixel format
            List<PixelFormats> pxfs = Enum.GetValues(typeof(PixelFormats)).Cast<PixelFormats>().ToList();
            foreach(PixelFormats pxf in pxfs) {
                pxfInput.Items.Add(EnumHelper.GetName(pxf));
            }
            if (pxfInput.Items.Contains(EnumHelper.GetName(PixelFormats.RGB24))) {
                pxfInput.SelectedIndex = pxfInput.Items.IndexOf(EnumHelper.GetName(PixelFormats.RGB24));
                pixelFormat = PixelFormats.RGB24;
            }
            //Resolution
            resControl.Resolution = new Vector2(1920, 1080);
            //Compression rate
            if (format == ImageFormat.JPG || format == ImageFormat.JPG2000) {
                jcrInput.IsEnabled = true;
                compressionLevel = 15;
                jcrInput.Value = 15;
                jcrInput.MinValue = 1;
                jcrInput.MaxValue = 31;
                jcrInput.ToolTip = "Zakres wartości 1 - 31";
            } else {
                jcrInput.IsEnabled = false;
                compressionLevel = null;
                jcrInput.ToolTip = "Tylko dla formatów z rodziny JPEG.";
            }
        }

        private void formatInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            List<ImageFormat> formats = Enum.GetValues(typeof(ImageFormat)).Cast<ImageFormat>().ToList();
            foreach (ImageFormat format in formats) {
                if(EnumHelper.GetName(format) == formatInput.SelectedItem.ToString()) {
                    this.format = format;
                    break;
                }
            }
            Refresh();
        }

        private void pxfInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            List<PixelFormats> pxfs = Enum.GetValues(typeof(PixelFormats)).Cast<PixelFormats>().ToList();
            foreach (PixelFormats pxf in pxfs) {
                if(EnumHelper.GetName(pxf) == pxfInput.SelectedItem.ToString()) {
                    pixelFormat = pxf;
                    break;
                }
            }
        }

        private void jcrInput_ValueChanged(object sender, EventArgs e) {
            compressionLevel = jcrInput.Value;
        }
    }
}
