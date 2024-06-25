using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Windows.Media.Imaging;

namespace WikiGeoWPF
{
    class HttpTools
    {
        private readonly static HttpClient client = new HttpClient();

        static HttpTools()
        {
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("WikiGeoGame/0.1 (brackmann.lukas@gmail.com)");
        }

        public static async Task<string> GetPageBody(string uri)
        {
            string responseBody = null;
            try
            {
                responseBody = await client.GetStringAsync(uri);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException caught!");
                Console.WriteLine("Message :{0},", e.Message);
            }

            return responseBody;
        }

        public static async Task<BitmapImage> GetImage (string uri)
        {
            Stream stream = null;
            try
            {
                stream = await client.GetStreamAsync(uri);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException caught!");
                Console.WriteLine("Message :{0}", e.Message);
                return null;
            }
            
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.StreamSource = stream;
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.EndInit();

            return img;

        }
    }
}
