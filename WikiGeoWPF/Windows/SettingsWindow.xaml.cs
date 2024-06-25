using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WikiGeoWPF
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        string[] languages;
        public SettingsWindow()
        {
            languages = Persistent.Settings.languages.Keys.ToArray();
            InitializeComponent();
            comboBoxLangs.ItemsSource = languages;
            comboBoxLangs.SelectedItem = Persistent.Settings.languagePlain;
        }

        private void ComboBoxLangs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Persistent.Settings.SetLanguage((string)comboBoxLangs.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HighScoreWindow highScoreWindow = new HighScoreWindow();
            highScoreWindow.ShowDialog();
        }
    }
}
