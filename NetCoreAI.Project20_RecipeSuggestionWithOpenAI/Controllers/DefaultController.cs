using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project20_RecipeSuggestionWithOpenAI.Models;

namespace NetCoreAI.Project20_RecipeSuggestionWithOpenAI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly OpanAIServece _opanAIServece;

        public DefaultController(OpanAIServece opanAIServece)
        {
            _opanAIServece = opanAIServece;
        }

        [HttpGet]
        public IActionResult CreateRecipe()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRecipe(string ingredients)
        {
            var result =await _opanAIServece.GetRecipeAsync(ingredients);
            ViewBag.recip = result;
            return View();
        }
    }
}
