using billige_madopskrifter.Service;
using billige_madopskrifter.Shared;
using Microsoft.AspNetCore.Mvc;

namespace billige_madopskrifter.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class LikedRecipesController
    {
        //IService obj
        private readonly ILikedRecipesService _likedRecipesService;

        //Constructor
        public LikedRecipesController(ILikedRecipesService likedRecipesService)
        {
            _likedRecipesService = likedRecipesService;
        }

        //Like / unlike recipe
        [Produces("application/json")]
        [HttpPost]
        public async Task<CreateLikedRecipeResponseDTO> LikeRecipe(CreateLikedRecipeRequestDTO dto)
        {
            return await _likedRecipesService.LikeRecipe(dto);
        }

        //Get recipes that a user have liked
        [Produces("application/json")]
        [HttpGet("{userId}")]
        public async Task<GetLikedRecipesByUserIdResponseDTO> GetLikedRecipesByUserId(int userId)
        {
            return await _likedRecipesService.GetLikedRecipesByUserId(userId);
        }

        //Check if a user have liked a recipe
        [Produces("application/json")]
        [HttpGet("byuseridandrecipeid/{userId:int}/{recipeId:int}")]
        public async Task<GetLikedRecipeByUserIdAndRecipeIdResponseDto> GetGetByUseridAndRecipeId(int userId, int recipeId)
        {
            return await _likedRecipesService.GetByUseridAndRecipeId(userId, recipeId);
        }
    }
}
