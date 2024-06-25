using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WikiGeoWPF
{
    /// <summary>
    /// This class stores information about the current game in an object.
    /// </summary>
    class Game
    {
        public Option mainArticle;
        public Option[] options = new Option[3];
        public int winner = -1;

        /// <summary>
        /// Initializer for the Game class.
        /// </summary>
        /// <param name="mainPage">Stores all information about the main article.</param>
        /// <param name="pages">Stores the answer options in an array.</param>
        public Game (Option mainPage, Option[] pages)
        {
            mainArticle = mainPage;
            options = pages;

            GetWinner();
        }

        /// <summary>
        /// This object holds all information about the main article and the options.
        /// </summary>
        public class Option
        {
            public double dist;
            public string title;
            public string imageUri;
            public BitmapImage image;
            public string description;
            public string extract;
            public double lat;
            public double lon;
            public string url;

            public Option(Page page)
            {
                dist = Convert.ToDouble(page.coordinates[0].dist, CultureInfo.InvariantCulture);
                title = page.title;
                imageUri = page.thumbnail?.source;
                description = page.description;
                extract = page.extract;
                lat = page.coordinates[0].lat;
                lon = page.coordinates[0].lon;
                url = page.fullurl;
            }


            public async Task<bool> SetImage()
            {
                if (!(imageUri is null))
                {
                    image = await HttpTools.GetImage(imageUri);
                    return true;
                }

                image = null;
                return false;
            }
        }

        public async static Task<Option> GetMainArticle()
        {
            List<Page> randomArticle = await Wikipedia.GetRandomArticlesWithCoordinates(1);
            Option randomArticleParsed = new Option(randomArticle[0]);
            return randomArticleParsed;
        }

        public async static Task<Option[]> GetOptions(double lat, double lon)
        {
            List<Page> randomArticles = await Wikipedia.GetRandomArticlesWithCoordinates(3, lat, lon);
            Option[] randomArtcilesParsed = new Option[3];
            for (int i = 0; i < 3; i++)
            {
                randomArtcilesParsed[i] = new Option(randomArticles[i]);
            }
            return randomArtcilesParsed;
        }

        public async static Task<Game> StartGame()
        {
            Option mainArticle = await GetMainArticle();
            Option[] optionArticles = await GetOptions(mainArticle.lat, mainArticle.lon);
            Game game = new Game(mainArticle, optionArticles);
            await game.SetImages();
            return game;
        }

        private void GetWinner()
        {
            double minValue = double.MaxValue;
            int minValueIndex = -1;
            for (int i = 0; i < 3; i++)
            {
                if(options[i].dist < minValue)
                {
                    minValue = options[i].dist;
                    minValueIndex = i;
                }
            }

            winner = minValueIndex;
        }

        public async Task SetImages()
        {
            await mainArticle.SetImage();

            foreach (Option option in options)
            {
                await option.SetImage();
            }
        }
    }
}
