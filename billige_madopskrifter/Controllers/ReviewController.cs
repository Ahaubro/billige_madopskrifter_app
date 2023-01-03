using billige_madopskrifter.Service;
using billige_madopskrifter.Shared;
using Microsoft.AspNetCore.Mvc;

namespace billige_madopskrifter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
      
        //IService obj
        private readonly IReviewService _reviewService;

        //Contructor
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        //Create new review
        [Produces("application/json")]
        [HttpPost]
        public async Task<CreateReviewResponseDTO> Create([FromBody] CreateReviewRequestDTO dto)
        {
            return await _reviewService.Create(dto);
        }

        //Get multiple reviews by recipeId
        [Produces("application/json")]
        [HttpGet("getByrecipeId/{recipeId:int}")]
        public async Task<GetReviewsByUserIDResponseDTO> GetByRecipeId(int recipeId)
        {
            return await _reviewService.GetReviewsByRecipeId(recipeId);
        }

        //Delete review
        [Produces("application/json")]
        [HttpDelete("{id:int}")]
        public async Task<DeleteReviewResponseDTO> Delete(int id)
        {
            return await _reviewService.Delete(id);
        }
    }
}
