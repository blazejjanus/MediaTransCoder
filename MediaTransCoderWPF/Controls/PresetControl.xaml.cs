using MediaTransCoder.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaTransCoder.WPF.Controls {
    /// <summary>
    /// Interaction logic for VideoPresetControl.xaml
    /// </summary>
    public partial class PresetControl : UserControl {
        public bool UsePreset {
            get {
                return presetCheck.IsChecked ?? usePreset;
            }
            private set {
                usePreset = value;
                ParentView?.Refresh();
            }
        }
        public PresetType Type { get; set; }
        internal IRefreshableControl? ParentView { get; set; }
        private PresetTarget target;
        private PresetQuality quality;
        private bool usePreset;

        public PresetControl() {
            InitializeComponent();
            target = PresetTarget.SIZE;
            quality = PresetQuality.HIGH;
            PreFillTemplate();
        }

        public Preset? GetPreset() {
            if(UsePreset == true) {
                return PresetsService.Get(Type, target, quality);
            } else {
                return null;
            }
        }

        private void PreFillTemplate() {
            //Use preset
            presetCheck.IsChecked = true;
            //Target
            List<PresetTarget> targets = Enum.GetValues(typeof(PresetTarget)).Cast<PresetTarget>().ToList();
            foreach (PresetTarget target in targets) {
                purposeInput.Items.Add(target.ToString());
            }
            if(purposeInput.Items.Contains(target.ToString())) {
                purposeInput.SelectedIndex = purposeInput.Items.IndexOf(target.ToString());
            }
            //Quality
            List<PresetQuality> qualities = Enum.GetValues(typeof(PresetQuality)).Cast<PresetQuality>().ToList();
            foreach (PresetQuality quality in qualities) {
                qualityInput.Items.Add(quality.ToString());
            }
            if (qualityInput.Items.Contains(quality.ToString())) {
                qualityInput.SelectedIndex = qualityInput.Items.IndexOf(quality.ToString());
            }
        }

        private void purposeInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            List<PresetTarget> targets = Enum.GetValues(typeof(PresetTarget)).Cast<PresetTarget>().ToList();
            foreach (PresetTarget target in targets) {
                if(target.ToString() == purposeInput.SelectedItem.ToString()) {
                    this.target = target;
                    break;
                }
            }
        }

        private void qualityInput_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            List<PresetQuality> qualities = Enum.GetValues(typeof(PresetQuality)).Cast<PresetQuality>().ToList();
            foreach (PresetQuality quality in qualities) {
                if (quality.ToString() == qualityInput.SelectedItem.ToString()) {
                    this.quality = quality;
                    break;
                }
            }
        }

        private void presetCheck_Checked(object sender, RoutedEventArgs e) {
            UsePreset = true;
            purposeInput.IsEnabled = true;
            qualityInput.IsEnabled = true;
        }

        private void presetCheck_Unchecked(object sender, RoutedEventArgs e) {
            UsePreset = false;
            purposeInput.IsEnabled = false;
            qualityInput.IsEnabled = false;
        }
    }
}
