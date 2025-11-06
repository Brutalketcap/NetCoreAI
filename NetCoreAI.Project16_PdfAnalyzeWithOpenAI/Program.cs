using Newtonsoft.Json;
using System.Text;
using UglyToad.PdfPig;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    private static readonly string apiKey = "";

    static async Task Main(string[] args)
    {
        Console.WriteLine("PDF Dosya Yolunu Giriniz: ");
        string pdfPath = Console.ReadLine();
        Console.WriteLine("Pdf Analizi AI tarafından yapılıyor...\n");

        string PdfText= ExtractTextFromPDF(pdfPath);
        await AnalayeWithAI(PdfText,"Pdf İçeriği");


        static string ExtractTextFromPDF(string FilePath)
        {
            StringBuilder text = new StringBuilder();

            using (PdfDocument pdf = PdfDocument.Open(FilePath))
            {
                foreach (var page in pdf.GetPages())
                {
                    text.AppendLine(page.Text);
                }
            }
            return text.ToString();
        }


        static async Task AnalayeWithAI(string text, string sourceType)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "chatgpt-4.5-turbo",
                    message = new[]
                    {
                    new { role = "system", content = "Sen bir yapay zeka asistanısın. Kulanıcının gönderdiiği metni analiz eder ve Türkçe olarak özetlersin. Yanıtlarını sadece Türkçe ver!" },
                    new { role = "user", content = $"Analyze and summarize the following {sourceType}:\n\n {text}" }
                    }
                };
                string json = JsonConvert.SerializeObject(requestBody);

                HttpContent content = new StringContent(json, Encoding.UTF8, "applicaion/json");
                HttpResponseMessage response = await client.PutAsync("https://api.openai.com/v1/chat/completions", content);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(responseJson))
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                   Console.WriteLine($"AI analiz {sourceType}: \n {result.Choices[0].message.content.ToString()}");
                }
                else
                {
                    Console.WriteLine($"Bir hata oluştu: {responseJson}");
                }

            }

        }
    }

}