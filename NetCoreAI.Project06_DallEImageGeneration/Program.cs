using Newtonsoft.Json;
using System.Text;

class Program
{
    static async Task Main(string[] asgs)
    {
        string apiKey = "";

        Console.WriteLine("Çizilmesini istediğiniz içerik (examle promts): ");
        string promt = Console.ReadLine();

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var request = new
            {
                promt,
                n = 1,
                size = "1024x1024"
            };

            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage respons = await client.PostAsync("https://api.openai.com/v1/images/generations", content);
            string responseString = await respons.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }

    }

}