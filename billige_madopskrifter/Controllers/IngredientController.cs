using billige_madopskrifter.Model;
using billige_madopskrifter.Service;
using billige_madopskrifter.Shared;
using Microsoft.AspNetCore.Mvc;

namespace billige_madopskrifter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : Controller
    {

        private readonly IIngredientService _ingredientService;

        //Constructor
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        //Create ingredient
        [Produces("application/json")]
        [HttpPost]
        public async Task<CreateIngredientResponseDTO> Create(CreateIngredientRequestDTO dto)
        {
            return await _ingredientService.Create(dto);
        }

        //Get all ingredients
        [Produces("application/json")]
        [HttpGet]
        public async Task<GetAllIngredientsReponseDTO> GetAll()
        {
            return await _ingredientService.GetAll();
        }

        //Get ingredient by id
        [Produces("application/json")]
        [HttpGet("{id:int}")]
        public async Task<GetIngredientByIdResponseDTO> GetById(int id)
        {
            return await _ingredientService.GetById(id);
        }

        //Get ingredients by RecipId
        [Produces("application/json")]
        [HttpGet("getbyrecipeid/{recipeId:int}")]
        public async Task<GetIngredientsByRecipeIDDTO> GetByRecipeId(int recipeId)
        {
            return await _ingredientService.GetByRecipeId(recipeId);
        }

        //Update ingredient
        [Produces("application/json")]
        [HttpPatch("{id:int}")]
        public async Task<UpdateIngredientResponseDTO> Update(UpdateIngredientRequestDTO dto, int id)
        {
            return await _ingredientService.Update(dto, id);
        }

        //Delete ingredient
        [Produces("application/json")]
        [HttpDelete("{id:int}")]
        public async Task<DeleteIngredientResponseDTO> Delete(int id)
        {
            return await _ingredientService.Delete(id);
        }

        //Search for ingredients
        [Produces("application/json")]
        [HttpGet("search/{search}")]
        public async Task<GetIngredientsBySearchQueryResponseDTO> SearchIngrediens(string search)
        {
            return await _ingredientService.SearchIngrediens(search);
        }

        [Produces("application/json")]
        [HttpGet("searchlist/{searchlist}")]
        public async Task<GetIngredientsBySearchQueryResponseDTO> SearchIngrediensByMultipleNames(string searchList)
        {
            return await _ingredientService.SearchIngrediensByMultipleNames(searchList);
        }
    }
}
