using Newtonsoft.Json;
using System.Text;
class Progra
{
    static async Task Main(string[] args)
    {
        Console.Write("Çeviri yapmak istediğiniz metini girin: ");
        string inputText = Console.ReadLine();

        string apiKey = "";

        string traslateText = await TranslateTextToEnglish(inputText, apiKey);

        if (!string.IsNullOrEmpty(traslateText))
        {
            Console.WriteLine($"İngilizceye Çevirilmiş metin: {traslateText}");
        }
        else
        {
            Console.WriteLine("Bir hata oluştu ");
        }
    }

    private static async Task<string> TranslateTextToEnglish(string Text, string apiKey)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer{apiKey}");

            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new {role="system", contet="You are a helpful translator."},
                    new {role="user", contet=$"Please traslate this text to Engilish: {Text}"}
                }
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                var responseMessages = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseString= await responseMessages.Content.ReadAsStringAsync();

                dynamic responseObje= JsonConvert.DeserializeObject(responseString);
                string translater= responseObje.choices[0].messages.content;
                return translater;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu {ex.Message}");
                return null;
                
            }

        }
        // Open ai api ile bir translate scripti


    }

}
