using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;

class Prgram
{
    static private readonly string apiKey = "";

    static async Task Main(string[] args)
    {
        Console.Write("Hikaye Türünü Seçin (Macera, Korku, Bilim Kurgu, Fantasik, Korku): ");
        string genre = Console.ReadLine();

        Console.Write("Ana karakter kim: ");
        string character = Console.ReadLine();

        Console.Write("Hikaye nerde geçiyo: ");
        string setting = Console.ReadLine();

        Console.Write("Hikaye uzunluğu (Kısa/Orta/Uzun): ");
        string length = Console.ReadLine();

        string prompt = $"{genre}Türünde olan bir hikaye yaz. Baş ana karakteri adı {character}. Hikaye {setting} bölgesinde geçiyor. {length} bir hikaye olucak. Giriş gelime sonuç içerecek.";

        string story = await GenerateStory(prompt);

        Console.WriteLine("\n --- AI Tarafından oluşturulan hikaye ---\n" + story);

    }

    static async Task<string> GenerateStory(string prompt)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        var requestBody = new
        {
            modle = "chatgpt-4.5-turbo",
            message = new[]
                {
                new { rol = "system", content = "You are a creative story writer." },
                new { rol = "user", content = prompt }
            },
            max_tokens = 100
        };

        var Jsoncontent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        HttpResponseMessage respons = await client.PutAsync("https://api.openai.com/v1/chat/completions", Jsoncontent);
        
        string jsonRespons= await respons.Content.ReadAsStringAsync();
        JsonDocument doc = JsonDocument.Parse(jsonRespons);
        return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
    }

}