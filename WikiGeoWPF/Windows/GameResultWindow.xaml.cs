using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WikiGeoWPF
{
    /// <summary>
    /// Interaktionslogik für GameResultWindow.xaml
    /// </summary>
    public partial class GameResultWindow : Window
    {
        string[] urls = new string[3];

        public GameResultWindow(int option)
        {
            InitializeComponent();
            SetWinner(option, Persistent.game.winner);
            ResultImg1.Source = Persistent.game.options[0].image;
            ResultImg2.Source = Persistent.game.options[1].image;
            ResultImg3.Source = Persistent.game.options[2].image;
            TxtResultTitle1.Text = Persistent.game.options[0].title;
            TxtResultTitle2.Text = Persistent.game.options[1].title;
            TxtResultTitle3.Text = Persistent.game.options[2].title;
            DistResult1.Text = string.Format("{0:0.00} km", Persistent.game.options[0].dist / 1000);
            DistResult2.Text = string.Format("{0:0.00} km", Persistent.game.options[1].dist / 1000);
            DistResult3.Text = string.Format("{0:0.00} km", Persistent.game.options[2].dist / 1000);
            for (int i = 0; i < 3; i++)
            {
                urls[i] = Persistent.game.options[i].url;
            }
        }



        public void SetWinner(int option, int winner)
        {
            if (option == winner)
            {
                bool newHighScore = Persistent.IncreaseStreak();
                TxtStreak.Text = "Streak: " + Persistent.streak;
                if (Persistent.streak > 4)
                    TxtStreak.Text += " 🔥";
                if (Persistent.streak > 9)
                    TxtStreak.Text += "🔥🔥";
                if (newHighScore)
                    TxtStreak.Text += "\nNew Highscore!";
                TxtWinLose.Text = "You win!";
            }
            else
            {
                if(Persistent.streak == 0)
                {
                    TxtStreak.Text = "";
                }
                else
                {
                    TxtStreak.Text = "Streak of " + Persistent.streak + " lost.";
                }


                Persistent.streak = 0;
                TxtWinLose.Text = "You lose!";

                switch (option)
                {
                    case 0:
                        BorderHighlight1.Style = (Style)Resources["BorderHighlightLose"];
                        break;
                    case 1:
                        BorderHighlight2.Style = (Style)Resources["BorderHighlightLose"];
                        break;
                    case 2:
                        BorderHighlight3.Style = (Style)Resources["BorderHighlightLose"];
                        break;
                    default:
                        break;
                }
            }

            switch (winner)
            {
                case 0:
                    BorderHighlight1.Style = (Style)Resources["BorderHighlightWin"];
                    break;
                case 1:
                    BorderHighlight2.Style = (Style)Resources["BorderHighlightWin"];
                    break;
                case 2:
                    BorderHighlight3.Style = (Style)Resources["BorderHighlightWin"];
                    break;
                default:
                    break;
            }
        }

        public void OpenUrl()
        {
            Console.WriteLine("clicky");
        }

        private void TxtResultTitle1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // System.Diagnostics.Process.Start(urls[0]);
            var psi = new ProcessStartInfo
            {
                FileName = urls[0],
                UseShellExecute = true
            };
            Process.Start(psi);
               
        }

        private void TxtResultTitle2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = urls[1],
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void TxtResultTitle3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = urls[2],
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
