using billige_madopskrifter.Service;
using billige_madopskrifter.Shared;
using Microsoft.AspNetCore.Mvc;

namespace billige_madopskrifter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;

        //Constructor
        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        //Create new recipe
        [Produces("application/json")]
        [HttpPost]
        public async Task<CreateRecipeResponseDTO> Create([FromBody] CreateRecipeRequestDTO dto)
        {
            return await _recipeService.Create(dto);
        }

        //Get all recipes
        [Produces("application/json")]
        [HttpGet]
        public async Task<GetAllRecipesResponseDTO> GetAll()
        { 
            return await _recipeService.GetAll();
        }

        //Get recipes by type and search query
        [Produces("application/json")]
        [HttpGet("search/{type}/{query}")]
        public async Task<GetRecipesByTypeAndSearchQueryResponseDTO> Search(string type, string query)
        {
            return await _recipeService.Search(type, query);
        }

        //Get recipe by id
        [Produces("application/json")]
        [HttpGet("{id:int}")]
        public async Task<GetRecipeByIdResponseDTO> GetRecipeById(int id)
        {
            return await _recipeService.GetById(id);
        }

        //Get recipe by UserId & name
        [Produces("application/json")]
        [HttpGet("getbynameanduserid/{userId:int}/{name}")]
        public async Task<GetByNameAndUserIdResponseDTO> GetByNameAndUserId(int userId, string name)
        {
            return await _recipeService.GetByNameAndUserId(userId, name);
        }

        //Get by userId
        [Produces("application/json")]
        [HttpGet("byuserid/{userId:int}")]
        public async Task<GetRecipesByUserIdResponseDTO> GetByUserId(int userid)
        {
            return await _recipeService.GetByUserId(userid);
        }

        //Get recipe by type
        [Produces("application/json")]
        [HttpGet("getbytype/{type}")]
        public async Task<GetRecipesByTypeResponseDTO> GetByType(string type)
        { 
            return await _recipeService.GetByType(type);
        }

        //Update recipe
        [Produces("application/json")]
        [HttpPatch("{id:int}")]
        public async Task<UpdateRecipeResponseDTO> Update([FromBody] UpdateRecipeRequestDTO dto, int id)
        { 
            return await _recipeService.Update(dto, id);
        }

        //Delete recipe
        [Produces("application/json")]
        [HttpDelete("{id:int}")]
        public async Task<DeleteRecipeReponseDTO> Delete(int id)
        {
            return await _recipeService.Delete(id);
        }

    }
}
