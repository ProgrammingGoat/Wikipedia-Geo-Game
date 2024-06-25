using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace WikiGeoWPF
{
    public class SearchResult
    {
        public Query query;
        public Error error;
    }

    public class Error
    {
        public string code;
        public string info;
        public string servedBy;
    }

    public class Query
    {
        public List<Search> search;
        public List<GeoSearch> geosearch;
        public Dictionary<string, Page> pages;
    }

    public class Search
    {
        public string ns;
        public string title;
        public string pageid;
        public string size;
        public string wordcount;
        public string snippet;
        public string timestamp;
    }

    public class GeoSearch
    {
        public int pageid;
        public int ns;
        public string title;
        public double lat;
        public double lon;
        public double dist;

    }

    public class Page
    {
        public int pageID;
        public int ns;
        public string title;
        public string description;
        public string extract;
        public Thumbnail thumbnail;
        public List<Coordinates> coordinates;
        public string fullurl;
    }

    public class Thumbnail
    {
        public string source;
        public string width;
        public string height;
    }

    public class Coordinates
    {
        public double lat;
        public double lon;
        public string globe;
        public string dist;
    }

    static class Wikipedia
    {

        public static async Task<string> QueryWikipedia(Dictionary<string, string> input)
        {
            string query = "";
            string language = Persistent.Settings.language;
            List<string> commands = new List<string>();

            foreach (KeyValuePair<string, string> pair in input)
            {
                commands.Add(Uri.EscapeDataString(pair.Key) + "=" + Uri.EscapeDataString(pair.Value));
            }

            query = string.Join("&", commands);
            string uri = "https://" + language + ".wikipedia.org/w/api.php?" + query;

            Console.WriteLine("Request sent: " + uri);

            string result = await HttpTools.GetPageBody(uri);

            return result;
        }


        /// <summary>
        /// Queries the API for a given number of articles that cointain coordinates/a geolocation.
        /// </summary>
        /// <param name="numberOfArticles">The number of articles to acquire.</param>
        /// <returns>A list of Page objects representing the articles.</returns>
        public static async Task<List<Page>> GetRandomArticlesWithCoordinates(int numberOfArticles, double lat = -1, double lon = -1)
        {
            List<Page> pages;
            List<Page> articles = new List<Page>();

            if (numberOfArticles > 5)
                return articles;

            int cycles = 0;

            while (articles.Count < numberOfArticles)
            {
                cycles++;
                if (cycles > 20)
                    break;
                Dictionary<string, string> query = new Dictionary<string, string>{
                    {"action", "query"},
                    {"format", "json" },
                    {"prop", "coordinates|description|extracts|pageimages|info" },
                    {"generator", "random" },
                    {"grnnamespace", "0" },
                    {"explaintext", "1" },
                    {"exintro", "1" },
                    {"piprop", "thumbnail" },
                    {"pithumbsize", "500" },
                    {"inprop", "url" },
                    {"grnlimit", "50" }
                };

                if (lat != -1 && lon != -1)
                {
                    query.Add("codistancefrompoint", string.Format(CultureInfo.InvariantCulture, "{0}|{1}", lat, lon));
                }
                
                string result = await QueryWikipedia(query);

                SearchResult searchresult = JsonConvert.DeserializeObject<SearchResult>(result);
                if (!(searchresult.error is null))
                {
                    return new List<Page>();
                }

                pages = searchresult.query.pages.Values.ToList();

                foreach (Page page in pages)
                {
                    if (!(page.coordinates is null))
                    {
                        articles.Add(page);
                        if (articles.Count >= numberOfArticles)
                            return articles;
                    }
                }

            }

            return articles;

        }

        public static async Task<BitmapImage> TryGetImage(Page page)
        {
            if (!(page.thumbnail is null))
            {
                return await HttpTools.GetImage(page.thumbnail.source);
            }
            return null;
        }
    }
}
