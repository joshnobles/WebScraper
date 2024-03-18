using System.Net.Http;
namespace WebScraper.Entities
{
    internal class Http
    {
        private static readonly HttpClient _client = new();

        public static string Get(string url)
        {
            if (!Valid.Url(url))
                return string.Empty;

            try
            {
                var res = _client.GetAsync(new Uri(url)).Result;

                if (!res.IsSuccessStatusCode)
                {
                    Logger.Log(LogType.ERROR, "[Http.Get] Bad Response Status Code: " + res.StatusCode);
                    
                    return string.Empty;
                }

                return res.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                Logger.Log(LogType.ERROR, "[Http.Get] " + e.StackTrace ?? e.Message);

                return string.Empty;
            }
        }

    }
}
