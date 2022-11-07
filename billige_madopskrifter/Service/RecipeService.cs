using billige_madopskrifter.Data;
using billige_madopskrifter.Model;
using billige_madopskrifter.Shared;
using Microsoft.EntityFrameworkCore;


namespace billige_madopskrifter.Service
{
    public interface IRecipeService
    {
        Task<CreateRecipeResponseDTO> Create(CreateRecipeRequestDTO dto);
        Task<GetAllRecipesResponseDTO> GetAll();
        Task<GetRecipeByIdResponseDTO> GetById(int id);
        Task<GetRecipesByUserIdResponseDTO> GetByUserId(int userId);
        Task<UpdateRecipeResponseDTO> Update(UpdateRecipeRequestDTO dto, int id);
        Task<DeleteRecipeReponseDTO> Delete(int id);
    }
    public class RecipeService : IRecipeService
    {

        private readonly DBContext _dbContext;
        private readonly IIngredientService _ingredientService;

        //Constructor
        public RecipeService(DBContext dbContext, IIngredientService ingredientService)
        {
            _dbContext = dbContext;
            _ingredientService = ingredientService;
        }

        //Create new recipe
        public async Task<CreateRecipeResponseDTO> Create(CreateRecipeRequestDTO dto)
        {
            var entity = _dbContext.Recipes.Add(new Recipe
            {
                Name = dto.Name,
                Type = dto.Type,
                PrepTime = dto.PrepTime,
                NumberOfPersons = dto.NumberOfPersons,
                UserId = dto.UserId,
            });

            await _dbContext.SaveChangesAsync();

            return new CreateRecipeResponseDTO
            {
                StatusText = "New recipe created",
                Name = entity.Entity.Name
            };
        }

        //Get all recipes
        public async Task<GetAllRecipesResponseDTO> GetAll()
        {
            var recipes = _dbContext.Recipes.ToList();

            return new GetAllRecipesResponseDTO
            {
                Recipes = recipes.Select(r => new RecipeDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Type = r.Type,
                    PrepTime = r.PrepTime,
                    NumberOfPersons= r.NumberOfPersons,
                    UserId=r.UserId,
                })
            };
        }

        //Get Recipe by Id
        public async Task<GetRecipeByIdResponseDTO> GetById(int id)
        {
            var recipe = _dbContext.Recipes.AsNoTracking().FirstOrDefault(r => r.Id == id);

            if (recipe != null) 
            {            
                return new GetRecipeByIdResponseDTO
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Type = recipe.Type,
                    PrepTime = recipe.PrepTime,
                    NumberOfPersons = recipe.NumberOfPersons,
                    UserId = recipe.UserId,
                    StatusText = "Succes recipe found"
                };
            }
            return new GetRecipeByIdResponseDTO { StatusText = "Error no recipe found" };
        }

        //Update recipe
        public async Task<UpdateRecipeResponseDTO> Update(UpdateRecipeRequestDTO dto, int id)
        {
            var recipe = await _dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            
            if (recipe != null)
            {
                recipe.Name = dto.Name;
                recipe.Type = dto.Type;
                recipe.PrepTime = dto.PrepTime;
                recipe.NumberOfPersons = dto.NumberOfPersons;
                recipe.UserId = dto.UserId;

                var entity = _dbContext.Update(recipe);
                await _dbContext.SaveChangesAsync();

                return new UpdateRecipeResponseDTO
                {
                    Name = dto.Name,
                    StatusText = "Succesfulle updatet recipe"
                };
            }

            return new UpdateRecipeResponseDTO
            {
                StatusText = "No recipe was found"
            };
        }

        //Delete recipe
        public async Task<DeleteRecipeReponseDTO> Delete(int id)
        { 
            var recipe = _dbContext.Recipes.FirstOrDefault(r => r.Id == id);

            if (recipe != null)
            {
                await _ingredientService.DeleteByRecipeId(id);
                _dbContext.Remove(recipe);
                await _dbContext.SaveChangesAsync();
                return new DeleteRecipeReponseDTO
                {
                    StatusText = "Succesfully deleted recipe"
                };
            }

            return new DeleteRecipeReponseDTO
            {
                StatusText = "Error no recipe found"
            };
        }

        //Get recipes by UserId
        public async Task<GetRecipesByUserIdResponseDTO> GetByUserId(int userId)
        {
            var recipes = _dbContext.Recipes.AsNoTracking().Where(r => r.UserId == userId);
            
            return new GetRecipesByUserIdResponseDTO
            {
                Recipes = recipes.Select(r => new RecipeDTO{
                    Id = r.Id,
                    Name = r.Name,
                    Type = r.Type,
                    PrepTime = r.PrepTime,
                    NumberOfPersons = r.NumberOfPersons,
                    UserId = r.UserId,
                })
            };            
        }
    }
}
