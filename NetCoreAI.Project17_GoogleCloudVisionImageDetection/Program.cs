using System.IO;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string googleApiKey = "";
    private static readonly string imgagePath = "";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Google Vision Api ile görsel nesne tesiti yapılır..");
        string response = await DetactObjects(imgagePath);

        Console.WriteLine("-----Tespit edilen Nesneler-----\n"+ response);

    }

    static async Task<string> DetactObjects(string path)
    {
        using var client = new HttpClient();
        string apiUrl = $"https://vision.googleapis.com/v1/images:annotate?key={googleApiKey}";

        byte[] imageBytes = File.ReadAllBytes(path);
        string base64Image = Convert.ToBase64String(imageBytes);

        var requestBody = new
        {
            requests = new[]
            {
                new
                {
                    image = new { content = base64Image },
                    features = new[] { new { type = "Label_Detection", maxResults = 10 } }
                }
            }
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        HttpResponseMessage reponse = await client.PostAsync(apiUrl, jsonContent);
        string reponseContent = await reponse.Content.ReadAsStringAsync();

        return reponseContent;
    }
}






