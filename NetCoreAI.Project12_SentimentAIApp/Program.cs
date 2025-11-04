using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

class Prgram
{
    private static readonly string apiKey = "";
    static async Task Main(string[] args)
    {

        Console.WriteLine("Lütfen mesajı giriniz: ");
        string input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine("\nDuygu analizi yapılıyor\n");
            string setiment = await AnalyzeSetnimet(input);

            Console.WriteLine($"Duygu analizi: {setiment}");
        }

        static async Task<string> AnalyzeSetnimet(string Text)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new {role = "system", content = "You are an AI that analyzes sentiment. You categorize text as Positive, Negative or Neutral"},
                        new {role = "user", content = $"Analyze the sentiment of this text: \"{Text}\" and return only Positive, Negative or Neutral"}

                    }
                };
                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    return result.choices[0].message.content.ToString();
                }
                else
                {
                    Console.WriteLine("Bir hata oluştu." + responseJson);
                    return "Hatta";
                }
            }
        }
        // girilen metinin analiz ederek olumlu, olumsuz veya nötr olduğunu belirler. openai chatgpt api kullanır.kodu tamamla.
    }
}