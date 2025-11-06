using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace NetCoreAI.Project20_RecipeSuggestionWithOpenAI.Models
{
    public class OpanAIServece
    {
        private readonly HttpClient _httpClient;

        private const string openAiUrl = "https://api.openai.com/v1/chat/completions";
        private const string apiKey = "";

        public OpanAIServece()//HttpClient httpClient
        {
            _httpClient = new HttpClient(); //httpClient
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        }

        public async Task<string> GetRecipeAsync(string ingredients)
        {
            var requesBody = new
            {
                model = "gpt-4o-mini",
                message = new[]
                {
                    new {role="system", content="Sen profesyonel bir haçısın. Kullanıcının elindeki malzemelere göre yemek tarif öner."},
                    new {role="user", content=$"Elimde şu malzemeler var: {ingredients}. Ne yapabilirim?" }
                },
                temperature = 0.7,
                max_tokens = 100
            };
            string jsonRequest = JsonConvert.SerializeObject(requesBody);
            var respons = await _httpClient.PostAsync(openAiUrl, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

            string responseBody = await respons.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(responseBody);
            return doc.RootElement.GetProperty("choices")[0].GetProperty("mesage").GetProperty("content").GetString();
        }
    }
}
