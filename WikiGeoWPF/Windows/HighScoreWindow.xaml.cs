using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für HighScores.xaml
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        public HighScoreWindow()
        {
            InitializeComponent();
            List<TestHighScores> highscores = new List<TestHighScores>
            {
                new TestHighScores("uwu", 69),
                new TestHighScores("owo", 420),
                new TestHighScores("awa", 1312)
            };

            DG_HighScores.DataContext = highscores;
        }

        public class TestHighScores
        {
            public string Name { get; set; }
            public int Score { get; set; }

            public TestHighScores(string name, int score)
            {
                this.Name = name;
                this.Score = score;
            }
        }
    }
}
