using billige_madopskrifter.Data;
using billige_madopskrifter.Migrations;
using billige_madopskrifter.Model;
using billige_madopskrifter.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Xml.Linq;

namespace billige_madopskrifter.Service
{
    //Interface implementation
    public interface IRecipeService
    {
        Task<CreateRecipeResponseDTO> Create(CreateRecipeRequestDTO dto);
        Task<GetAllRecipesResponseDTO> GetAll();
        Task<GetRecipeByIdResponseDTO> GetById(int id);
        Task<GetRecipesByUserIdResponseDTO> GetByUserId(int userId);
        Task<GetByNameAndUserIdResponseDTO> GetByNameAndUserId(int userId, string name);
        Task<UpdateRecipeResponseDTO> Update(UpdateRecipeRequestDTO dto, int id);
        Task<DeleteRecipeReponseDTO> Delete(int id);
        Task<GetRecipesByTypeResponseDTO> GetByType(string type);
        Task<GetRecipesByTypeAndSearchQueryResponseDTO> Search(string type, string query);
        Task<UpdateDescriptionResponseDTO> UpdateRecipeDescription(UpdateDescriptionRequestDTO dto, int id);
    }
    public class RecipeService : IRecipeService
    {

        //Db & IIngrediensService obj
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
            var check = _dbContext.Recipes.AsNoTracking().FirstOrDefault(r => r.UserId == dto.UserId && r.Name == dto.Name);

            if (check == null)
            {
                var entity = _dbContext.Recipes.Add(new Recipe
                {
                    Name = dto.Name,
                    Type = dto.Type,
                    PrepTime = dto.PrepTime,
                    NumberOfPersons = dto.NumberOfPersons,
                    EstimatedPrice = dto.EstimatedPrice,
                    UserId = dto.UserId,
                });

                await _dbContext.SaveChangesAsync();

                return new CreateRecipeResponseDTO
                {
                    StatusText = "New recipe created",
                    Name = entity.Entity.Name
                };
            }

            return new CreateRecipeResponseDTO
            { 
                StatusText = "You already created a recipe by that name",
                Name = dto.Name,
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
                    EstimatedPrice = r.EstimatedPrice,
                    Description = r.Description,
                    UserId=r.UserId,
                })
            };
        }

        //GetByNameAndUserId
        public async Task<GetByNameAndUserIdResponseDTO> GetByNameAndUserId(int userId, string name)
        {
            var recipe = _dbContext.Recipes.AsNoTracking().FirstOrDefault(r => r.UserId == userId && r.Name == name);

            if (recipe != null)
            {
                return new GetByNameAndUserIdResponseDTO
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Type = recipe.Type,
                    PrepTime = recipe.PrepTime,
                    NumberOfPersons = recipe.NumberOfPersons,
                    EstimatedPrice = recipe.EstimatedPrice,
                    UserId = recipe.UserId,
                    StatusText = "Succes recipe found"
                };
            }
            return new GetByNameAndUserIdResponseDTO { StatusText = "Error no recipe found" };
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
                    EstimatedPrice = recipe.EstimatedPrice,
                    Description = recipe.Description,
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
                recipe.Description = dto.Description;
                recipe.UserId = dto.UserId;
                recipe.PrepTime = dto.PrepTime;
                recipe.EstimatedPrice = dto.EstimatedPrice;
                recipe.NumberOfPersons = dto.NumberOfPersons;
                

                var entity = _dbContext.Update(recipe);
                await _dbContext.SaveChangesAsync();

                return new UpdateRecipeResponseDTO
                {
                    StatusText = "Succesfulle updatet recipe"
                };
            }

            return new UpdateRecipeResponseDTO
            {
                StatusText = "No recipe was found"
            };
        }

        //Update recipe - description only
        public async Task<UpdateDescriptionResponseDTO> UpdateRecipeDescription(UpdateDescriptionRequestDTO dto, int id)
        {
            var recipe = await _dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == id);

            if (recipe != null)
            {

                recipe.Description = dto.Description;



                var entity = _dbContext.Update(recipe);
                await _dbContext.SaveChangesAsync();

                return new UpdateDescriptionResponseDTO
                {
                    StatusText = "Succesfulle updatet recipe"
                };
            }

            return new UpdateDescriptionResponseDTO
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

            if (recipes != null)
            {
                return new GetRecipesByUserIdResponseDTO
                {
                    Recipes = recipes.Select(r => new RecipeDTO
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
            }

            return null;
        }

        //Get recipes by type
        public async Task<GetRecipesByTypeResponseDTO> GetByType(string type)
        {
            var recipes = _dbContext.Recipes.AsNoTracking().Where(r => r.Type == type);

            if (recipes != null)
            {
                return new GetRecipesByTypeResponseDTO
                {
                    Recipes = recipes.Select(r => new RecipeDTO
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
            }

            return null;
        }

        //Search for recipes using type and name
        public async Task<GetRecipesByTypeAndSearchQueryResponseDTO> Search(string type, string query)
        {
            var recipes = _dbContext.Recipes.AsNoTracking().Where(r => r.Type == type);

            if (recipes != null)
            {
                recipes = recipes.Where(r => r.Name.Contains(query));

                return new GetRecipesByTypeAndSearchQueryResponseDTO
                {
                    Recipes = recipes.Select(r => new RecipeDTO
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
            }
            return null;
        }

    }
}
