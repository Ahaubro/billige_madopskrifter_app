using billige_madopskrifter.Data;
using billige_madopskrifter.Model;
using billige_madopskrifter.Shared;
using Microsoft.EntityFrameworkCore;

namespace billige_madopskrifter.Service
{
    
    //Interface implementation
    public interface IReviewService
    {
        Task<GetAllReviewsDTOReponseDTO> GetAll();
        Task<GetReviewsByUserIDResponseDTO> GetReviewsByRecipeId(int recipeId);
        Task<CreateReviewResponseDTO> Create(CreateReviewRequestDTO dto);
        Task<DeleteReviewResponseDTO> Delete(int id);
    }
    public class ReviewService : IReviewService
    {
        //Db object
        private readonly DBContext _dbContext;

        //Constructor
        public ReviewService(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        // Get all reviews
        public async Task<GetAllReviewsDTOReponseDTO> GetAll()
        {
            var reviews = _dbContext.Reviews.ToList();

            return new GetAllReviewsDTOReponseDTO
            {
                Reviews = reviews.Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    RecipeId = r.RecipeId,
                    Content = r.Content,
                    Rating = r.Rating
                })
            };
        }

        // Get multiple reviews by userId
        public async Task<GetReviewsByUserIDResponseDTO> GetReviewsByRecipeId(int recipeId)
        {
            var reviews = _dbContext.Reviews.AsNoTracking().Where(r => r.RecipeId == recipeId);

            if (reviews != null)
            {
                return new GetReviewsByUserIDResponseDTO
                {
                    Reviews = reviews.Select(r => new ReviewDTO
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        RecipeId = r.RecipeId,
                        Content = r.Content,
                        Rating = r.Rating
                    })
                };
            }

            return null;
        }

        // Create new review
        public async Task<CreateReviewResponseDTO> Create(CreateReviewRequestDTO dto)
        {
            var entity = _dbContext.Reviews.Add(new Review
            {
                UserId = dto.UserId,
                RecipeId = dto.RecipeId,
                Content = dto.Content,
                Rating = dto.Rating
            });


            await _dbContext.SaveChangesAsync();

            return new CreateReviewResponseDTO
            {
                StatusText = "New review created",
            };
        }

        //Delete review
        public async Task<DeleteReviewResponseDTO> Delete(int id)
        {
            var review = _dbContext.Reviews.FirstOrDefault(r => r.Id == id);

            if (review != null)
            {
                _dbContext.Remove(review);
                await _dbContext.SaveChangesAsync();
                return new DeleteReviewResponseDTO
                {
                    StatusText = "Succesfully deleted review"
                };
            }

            return new DeleteReviewResponseDTO
            {
                StatusText = "Error no review found"
            };
        }

    }
}

