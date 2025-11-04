using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var apiKey = "";

        Console.WriteLine("Lütfen sorunuzu giriniz:(Örnek:'Merhaba bu gün hava İstanbul'da kaç derece')");
        var prompt= Console.ReadLine();

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new {role="system", content= "Your are a helpful assistant"},
                new {role="user", content=prompt}
            },
            max_tokens= 100,
        };
        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8,"application/json");

        try
        {
            var response = await httpClient.PutAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                Console.WriteLine("Open AI'nin Cevabı; ");
                Console.WriteLine(answer);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bir hata oluştu {ex.Message}");
        }
    }
}