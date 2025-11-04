
using NetCoreAI.Project03_RapidApi.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;

var client = new HttpClient();
List<ApiSeriesViewModel> apiSeriesViewModels = new List<ApiSeriesViewModel>();

var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
    Headers =
    {
        { "x-rapidapi-key", "eeaa9f9223mshce949ca8e3abd09p160763jsn3eabe1800b1e" },
        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    apiSeriesViewModels = JsonConvert.DeserializeObject<List<ApiSeriesViewModel>>(body);
    foreach (var Series in apiSeriesViewModels)
    {
        Console.WriteLine(Series.rank + " " + Series.title + " " + Series.rating + " " + Series.year + " ");
    }
    Console.WriteLine(body);
}
Console.ReadLine();