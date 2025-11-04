using Newtonsoft.Json;
using System.Text;

class Prgram
{
    private static readonly string apikey = "";
    static async Task Main(string[] args)
    {
        Console.WriteLine("Bir metin girin");
        string input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Gelişmiş duygu analizi yapılıyor...");
            string sentiment = await AdvancedSentimentalAnalysis(input);
            Console.WriteLine($"Duygu analiziniz: {sentiment}");
        
        }

        static async Task<string> AdvancedSentimentalAnalysis(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer{apikey}");

                var requesBody = new
                {
                    model = "gpt-3.5-turbo",
                    message = new[]
                    {
                        new { role = "system", content = "You are an advanced AI that analyzes emotions in text. Your response must be in JSON format. Identiyf the sentiment scores (0-100%) for the following emotions: Joy, Sadness, Anger, Fear, Surprise and Neutral." },
                        new { role = "user", content = "Analyze this text: \"{text}\" and return a JSON object with percentages for each emotions." }
                    }
                };

                string json = JsonConvert.SerializeObject(requesBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync("https://api.openai.com/v1/chat/completions", content);
                string responsJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responsJson);
                    return result.choices[0].message.content.ToString();
                }
                else
                {
                    Console.WriteLine("Bir hata oluştu" + responsJson);
                    return "Hata!";
                }
            }

        }
    }
}

