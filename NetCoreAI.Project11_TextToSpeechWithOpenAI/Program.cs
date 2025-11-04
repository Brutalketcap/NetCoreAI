using Newtonsoft.Json;
using System.Text;

class Prgram
{
    private static readonly string apiKey = "";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Metin Girin: ");
        string input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Ses doyasını oluşturuluyor...");
            await GenerateSpeeck(input);
            Console.WriteLine("Ses doyasi 'output mp3' olarak kaydediniz.");
            System.Diagnostics.Process.Start("explorer.exe", "output.mp3");
        }
    }
    static async Task GenerateSpeeck(string Text)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "tts-1",
                input = Text,
                voice = "alloy"
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);
            StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/audio/speech", content);
            if (response.IsSuccessStatusCode)
            {
                byte[] audioBytes = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync("output.mp3", audioBytes); 
            }
            else
            {
                Console.WriteLine($"Bir Hata Oluştu: {response.StatusCode}");
            }
        }
    }
}