using System;
using System.Windows;
using System.Threading.Tasks;

namespace WikiGeoWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnStartGame_Click(object sender, RoutedEventArgs e)
        {
            await StartGame();
        }

        private async void BtnGuess1_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtons(false);
            GameResultWindow window = new GameResultWindow(0)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Topmost = true
            };
            window.Show();
            await StartGame();

        }

        private async void BtnGuess2_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtons(false);
            GameResultWindow window = new GameResultWindow(1)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Topmost = true
            };
            window.Show();
            await StartGame();
        }

        private async void BtnGuess3_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtons(false);
            GameResultWindow window = new GameResultWindow(2)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Topmost = true
            };
            window.Show();
            await StartGame();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private void ToggleButtons(bool onOrOff)
        {
            BtnGuess1.IsEnabled = onOrOff;
            BtnGuess2.IsEnabled = onOrOff;
            BtnGuess3.IsEnabled = onOrOff;
        }

        private async Task StartGame()
        {
            LoadingWindow loadingWindow = new LoadingWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            mainWindow.IsEnabled = false;
            loadingWindow.Show();


            Persistent.game = await Game.StartGame();

            BtnStartGame.IsEnabled = true;

            TxtTitle.Text = Persistent.game.mainArticle.title;
            TxtMainArticle.Text = Persistent.game.mainArticle.extract;

            ImgArticlePicture.Source = Persistent.game.mainArticle.image;

            ImgGuess1.Source = Persistent.game.options[0].image;
            BtnGuess1.Content = Persistent.game.options[0].title;
            TxtInfo1.Text = Persistent.game.options[0].description;

            ImgGuess2.Source = Persistent.game.options[1].image;
            BtnGuess2.Content = Persistent.game.options[1].title;
            TxtInfo2.Text = Persistent.game.options[1].description;

            ImgGuess3.Source = Persistent.game.options[2].image;
            BtnGuess3.Content = Persistent.game.options[2].title;
            TxtInfo3.Text = Persistent.game.options[2].description;


            loadingWindow.Owner.Activate();
            loadingWindow.Close();
            mainWindow.IsEnabled = true;
            ToggleButtons(true);
        }
        
    }
}
