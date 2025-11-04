using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Net;
using System.Text;

class Prgram
{
    private static string apiKey = "";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Lütfen analiz yamak istediğinzi web sayfasını Url'ini giriniz");
        string input = Console.ReadLine();
        Console.WriteLine("\n web sayfası içeriğini: ");

        string webContent = ExtractTextFromWeb(input);
        await AnalayzeWithAI(webContent, "web sayafsı içeriği");

        static string ExtractTextFromWeb(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var boytext = doc.DocumentNode.SelectSingleNode("//body")?.InnerText;
            return boytext ?? "Sayfa içeriği okunamadı.";
        }

    }
    static async Task AnalayzeWithAI(string text, string soureType)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "Sen bir yapay zeka asistanısın. Kulanıcının gönderdiiği metni analiz eder ve Türkçe olarak özetlersin. Yanıtlarını sadece Türkçe ver!" },
                    new { role = "user", content = $"Analyze and summarize the following {soureType}:\n\n {text}" }
                }
            };
            string json = JsonConvert.SerializeObject(requestBody);
            HttpContent content= new StringContent(json, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await client.PutAsync("https://api.openai.com/v1/chat/completions", content);

            string responseJson= await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseJson))
            {
                var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                Console.WriteLine($"\n AI Analizi({soureType}):\n({result.choices[0].message.content})");
            }
            else
            {
                Console.WriteLine("Hata oluştu" + responseJson);
            }
        }

    }
}