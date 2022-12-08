using billige_madopskrifter.Data;
using billige_madopskrifter.Model;
using billige_madopskrifter.Shared;
using Microsoft.EntityFrameworkCore;

namespace billige_madopskrifter.Service
{
    public interface ILikedRecipesService
    {
        //Interface implementation
        Task<CreateLikedRecipeResponseDTO> LikeRecipe(CreateLikedRecipeRequestDTO dto);
        Task<GetLikedRecipesByUserIdResponseDTO> GetLikedRecipesByUserId(int userId);
        Task<GetLikedRecipeByUserIdAndRecipeIdResponseDto> GetByUseridAndRecipeId(int userId, int recipeId);
    }
    public class LikedRecipesService : ILikedRecipesService
    {
        //Db obj
        private readonly DBContext _dbContext;

        //Constructor
        public LikedRecipesService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Jeg har valgt at ændre funktionen sådan at den både liker og unliker en opskrift, så man på den måde nemt kan
        // bruge den samme knap i frontend til henholdsvis at like eller unlike
        public async Task<CreateLikedRecipeResponseDTO> LikeRecipe(CreateLikedRecipeRequestDTO dto)
        {
            var check = _dbContext.LikedRecipes.AsNoTracking().FirstOrDefault(lr => lr.UserId == dto.UserId && lr.RecipeId == dto.RecipeId);

            if (check == null)
            {  
                var likedRecipe = _dbContext.LikedRecipes.Add(new LikedRecipes
                {
                    UserId = dto.UserId,
                    RecipeId = dto.RecipeId,
                });

                await _dbContext.SaveChangesAsync();

                return new CreateLikedRecipeResponseDTO
                {
                    StatusText = "Succesfully liked recipe"
                };
            }

            _dbContext.LikedRecipes.Remove(check);

            await _dbContext.SaveChangesAsync();

            return new CreateLikedRecipeResponseDTO
            {
                StatusText = "Removed liked recipe"
            };
        }

        //Der læses først likedRecipes´med userId, herefter bruges recipeId på likedRecipe objektet
        //til at læse opskrifter fra Recipe tabellen, som gemmes på en liste, som så returneres
        //(Get recipes from likedRecipes by userId)
        public async Task<GetLikedRecipesByUserIdResponseDTO> GetLikedRecipesByUserId(int userId)
        {
            var likedRecipes = _dbContext.LikedRecipes.AsNoTracking().Where(lr => lr.UserId == userId).ToList();

            var recipes = _dbContext.Recipes.AsNoTracking();
            var recipesList = _dbContext.Recipes.ToList(); 
            recipesList.Clear();

            foreach (var lr in likedRecipes)
            {

                recipes = _dbContext.Recipes.AsNoTracking().Where(recipe => recipe.Id == lr.RecipeId);

                recipes.ToList().ForEach( (recipe) => { 
                    recipesList.Add(recipe); 
                });                          

            }

            return new GetLikedRecipesByUserIdResponseDTO
            {
                Recipes = recipesList.Select(r => new RecipeDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Type = r.Type,
                    PrepTime = r.PrepTime,
                    NumberOfPersons = r.NumberOfPersons,
                    EstimatedPrice = r.EstimatedPrice,
                    Description = r.Description,
                    UserId = r.UserId,
                })
            };

            return null;
        }

        //Get likedRecipe by UserId and Recipeid (to check if the user have liked the recipe)
        public async Task<GetLikedRecipeByUserIdAndRecipeIdResponseDto> GetByUseridAndRecipeId(int userId, int recipeId)
        {
            var likedRecipe = _dbContext.LikedRecipes.AsNoTracking().FirstOrDefault(lr => lr.UserId == userId && lr.RecipeId == recipeId);

            if (likedRecipe != null)
            {
                return new GetLikedRecipeByUserIdAndRecipeIdResponseDto
                {
                    Id = likedRecipe.Id,
                    UserId = likedRecipe.UserId,
                    RecipeId = recipeId,
                    StatusText = "isLiked"
                };
            }
            return new GetLikedRecipeByUserIdAndRecipeIdResponseDto
            {
                StatusText = "notLiked"
            };


        }

    }
}
