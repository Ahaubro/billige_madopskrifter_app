using billige_madopskrifter.Service;
using billige_madopskrifter.Shared;
using Microsoft.AspNetCore.Mvc;

namespace billige_madopskrifter.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class LikedRecipesController
    {
        private readonly ILikedRecipesService _likedRecipesService;

        public LikedRecipesController(ILikedRecipesService likedRecipesService)
        {
            _likedRecipesService = likedRecipesService;
        }

        [Produces("application/json")]
        [HttpPost]
        public async Task<CreateLikedRecipeResponseDTO> LikeRecipe(CreateLikedRecipeRequestDTO dto)
        {
            return await _likedRecipesService.LikeRecipe(dto);
        }

        [Produces("application/json")]
        [HttpGet("{userId}")]
        public async Task<GetLikedRecipesByUserIdResponseDTO> GetLikedRecipesByUserId(int userId)
        {
            return await _likedRecipesService.GetLikedRecipesByUserId(userId);
        }
    }
}
