using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;

namespace CBZCreator
{
    public static class CoverFetcher
    {
        public static string DownloadCover(string isbn, string downloadLocation, string apiKey)
        {
            return DownloadCoverAsync(isbn, downloadLocation, apiKey).Result;
        }

        public static async Task<string> DownloadCoverAsync(string isbn, string downloadLocation, string apiKey)
        {
            CustomSearchAPIService css = new CustomSearchAPIService(new BaseClientService.Initializer() { ApiKey = apiKey });

            var listRequest = css.Cse.List();
            listRequest.Cx = "856df5d4fa115864d";
            listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            listRequest.Start = 1;
            listRequest.Num = 5;
            listRequest.Q = isbn;

            Console.WriteLine("Searching online for cover...");
            var search_results = await listRequest.ExecuteAsync();

            if (search_results.Items == null || search_results.Items.Count < 1)
            {
                Console.WriteLine("Unable to find cover.");
                return string.Empty;
            }

            int largestIndex = 0;
            for (int i = 1; i < search_results.Items.Count; ++i)
            {
                var result = search_results.Items[i];

                if (result.Image.Height > search_results.Items[largestIndex].Image.Height)
                {
                    largestIndex = i;
                }
            }

            string link = search_results.Items[largestIndex].Link;

            try
            {
                string ext = link[link.LastIndexOf('.')..];
                string saveTo = $"{downloadLocation}\\0000_cover{ext}";

                using (var httpClient = new HttpClient() { BaseAddress = new Uri(link), })
                {
                    var resp = await httpClient.SendAsync(new HttpRequestMessage());
                    
                    if (!resp.IsSuccessStatusCode)
                    {
                        throw new Exception($"HTTP response code: {resp.StatusCode}");
                    }

                    await File.WriteAllBytesAsync(saveTo, await resp.Content.ReadAsByteArrayAsync());
                }

                Console.WriteLine("Cover downloaded successfully!");
                return saveTo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Downloading cover from '{link}' failed.{(Globals.VerboseLogging ? $"\r\n{ex.Message}" : string.Empty)}");
                return string.Empty;
            }
        }
    }
}
