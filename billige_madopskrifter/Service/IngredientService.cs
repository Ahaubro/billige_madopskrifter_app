using billige_madopskrifter.Data;
using billige_madopskrifter.Model;
using billige_madopskrifter.Shared;
using Microsoft.EntityFrameworkCore;

namespace billige_madopskrifter.Service
{
    public interface IIngredientService
    {
        Task<CreateIngredientResponseDTO> Create(CreateIngredientRequestDTO dto);
        Task<GetAllIngredientsReponseDTO> GetAll();
        Task<GetIngredientByIdResponseDTO> GetById(int id);
        Task<UpdateIngredientResponseDTO> Update(UpdateIngredientRequestDTO dto, int id);
        Task<DeleteIngredientResponseDTO> Delete(int id);
        Task<GetIngredientsByRecipeIDDTO> GetByRecipeId(int recipeId);
        Task<DeleteIngredientByRecipeIdResponseDTO> DeleteByRecipeId(int recipeId);
    }

    public class IngredientService : IIngredientService
    {
        private readonly DBContext _dbContext;

        //Constructor
        public IngredientService(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        //Create ingredient
        public async Task<CreateIngredientResponseDTO> Create(CreateIngredientRequestDTO dto)
        {
            var entity = _dbContext.Ingredients.Add(new Ingredient
            { 
                RecipeId = dto.RecipeId,
                Name = dto.Name,
                Type = dto.Type,
                MeasurementUnit = dto.MeasurementUnit,
                Amount = dto.Amount,
                Alergene = dto.Alergene,
            });

            await _dbContext.SaveChangesAsync();

            return new CreateIngredientResponseDTO
            {
                Name = dto.Name,
                StatusText = "New ingredient created"
            };
        }

        //Get all ingredients
        public async Task<GetAllIngredientsReponseDTO> GetAll()
        {
            var ingredients = _dbContext.Ingredients.ToList();

            return new GetAllIngredientsReponseDTO
            {
                Ingredients = ingredients.Select(i => new Ingredient
                {
                    Id = i.Id,
                    RecipeId = i.RecipeId,
                    Name = i.Name,
                    Type = i.Type,
                    MeasurementUnit= i.MeasurementUnit,
                    Amount= i.Amount,
                    Alergene = i.Alergene
                })
            };
        }

        //Get Ingredient by Id
        public async Task<GetIngredientByIdResponseDTO> GetById(int id)
        {
            var ingredient = _dbContext.Ingredients.AsNoTracking().FirstOrDefault(r => r.Id == id);

            if (ingredient != null)
            {
                return new GetIngredientByIdResponseDTO
                {
                    Id = ingredient.Id,
                    RecipeId= ingredient.RecipeId,
                    Name = ingredient.Name,
                    Type = ingredient.Type,
                    MeasurementUnit = ingredient.MeasurementUnit,
                    Amount = ingredient.Amount,
                    Alergene = ingredient.Alergene,
                    StatusText = "Succes ingredient found"
                };
            }
            return new GetIngredientByIdResponseDTO { StatusText = "Error no ingredient found" };
        }

        //Update ingredient
        public async Task<UpdateIngredientResponseDTO> Update(UpdateIngredientRequestDTO dto, int id)
        {
            var ingredient = await _dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient != null)
            {
                ingredient.Name = dto.Name;
                ingredient.Type = dto.Type;
                ingredient.MeasurementUnit = dto.MeasurementUnit;
                ingredient.Amount = dto.Amount;
                ingredient.Alergene = dto.Alergene;

                var entity = _dbContext.Update(ingredient);
                await _dbContext.SaveChangesAsync();

                return new UpdateIngredientResponseDTO
                {
                    Name = dto.Name,
                    StatusText = "Succesfully updatet ingredient"
                };
            }

            return new UpdateIngredientResponseDTO { StatusText = "No ingredient was found" };
        }

        //Delete ingredient
        public async Task<DeleteIngredientResponseDTO> Delete(int id)
        {
            var entity = _dbContext.Ingredients.FirstOrDefault(i => i.Id == id);

            if (entity != null)
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return new DeleteIngredientResponseDTO { StatusText = "Succesfully deleted" };
            }

            return new DeleteIngredientResponseDTO { StatusText = "Error - No entity found" };
        }

        //get ingredient by RecipeId
        public async Task<GetIngredientsByRecipeIDDTO> GetByRecipeId(int recipeId)
        {
            var ingredients = _dbContext.Ingredients.AsNoTracking().Where(i => i.RecipeId == recipeId);

            return new GetIngredientsByRecipeIDDTO
            {
                Ingredients = ingredients.Select(i => new IngredientDTO
                {
                    Id = i.Id,
                    RecipeId = i.RecipeId,
                    Name = i.Name,
                    Type = i.Type,
                    MeasurementUnit = i.MeasurementUnit,
                    Amount = i.Amount,
                    Alergene = i.Alergene
                })
            };
        }

        //Delete multiple ingredients by RecipeId(If a recipe is deleted)
        public async Task<DeleteIngredientByRecipeIdResponseDTO> DeleteByRecipeId(int recipeId)
        {
            var ingredients = _dbContext.Ingredients.AsNoTracking().Where(i => i.RecipeId == recipeId);

            if (ingredients != null)
            {       
                _dbContext.Ingredients.RemoveRange(ingredients);
                await _dbContext.SaveChangesAsync();

                return new DeleteIngredientByRecipeIdResponseDTO
                {
                    StatusText = "Ingredients deleted"
                };
            }

            return new DeleteIngredientByRecipeIdResponseDTO
            {
                StatusText = "Error - no ingredients to delete"
            };
        } 
    }
}
