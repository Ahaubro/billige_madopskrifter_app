using billige_madopskrifter.Data;
using billige_madopskrifter.Model;
using billige_madopskrifter.Shared;
using Microsoft.EntityFrameworkCore;

namespace billige_madopskrifter.Service
{
    public interface ILikedRecipesService
    {
        Task<CreateLikedRecipeResponseDTO> LikeRecipe(CreateLikedRecipeRequestDTO dto);
        Task<GetLikedRecipesByUserIdResponseDTO> GetLikedRecipesByUserId(int userId);
    }
    public class LikedRecipesService : ILikedRecipesService
    {
        private readonly DBContext _dbContext;

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

        //Den var lidt pebberet, godt fucking arbejde Alex <3 - Der læses først likedRecipes´med userId, herefter bruges recipeId på likedRecipe objektet
        //til at læse opskrifter fra Recipe tabellen, som gemmes på en liste, som så returneres
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

            //System.Diagnostics.Debug.WriteLine("Udenfor loop", recipesList.ToString());

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

    }
}
