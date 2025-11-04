using Newtonsoft.Json;
using System.Text;

class Prgram
{
    ///////////// bakılacak satırlar var.

    private static string apiKey = "";
    static async Task Main(string[] args)
    {
        Console.WriteLine("Uzun metninizi veya makalenizi giriniz: ");
        string input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine("\nGirmiş olduğunuz metin AI tarafından özetleniyor...\n");

            string shortSummary = await SummarizeText(input, "short");
            string mediumSummary = await SummarizeText(input, "medium");
            string detailedummary = await SummarizeText(input, "detail");
            Console.WriteLine($"Özetler\n ** Kısa özet **{shortSummary}\n **Orata özet**{mediumSummary}\n **Detaylı özet** {detailedummary}" );
        }

        async Task<string> SummarizeText(string text, string level)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                string instruction = level switch
                {
                    "short" => "Summarize this text in 1-3 sentences.",
                    "medium" => "Summarize this text in 3-5 sentences.",
                    "detailed" => "Summarize this text in a detailed but concise manner.",
                    _ => "Summerize this text."
                };

                var requestBody = new
                {
                    model = "gpt-3.5-turboa",
                    message = new[]
                    {
                        new { role = "systme", content = "You are an AI that summarize text info different level: short, medium and detailed." },
                        new { role = "user", content = $"{instruction}\n\n{text}" }
                    }
                };

                var json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync("https://api.openai.com/v1/chat/completions", content);

                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    return result.choices[0].message.content.ToString();
                }
                else
                {
                    Console.WriteLine("Bir hata oluştu");
                    return "Hata!!";
                }


            }

        }
    }
}