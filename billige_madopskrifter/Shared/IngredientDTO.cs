using billige_madopskrifter.Model;
using Microsoft.Extensions.Primitives;

namespace billige_madopskrifter.Shared
{
    public class IngredientDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string MeasurementUnit { get; set; }
        public double Amount { get; set; }
        public string Alergene { get; set; }
    }

    //Get all ingredients response dto
    public class GetAllIngredientsReponseDTO
    { 
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }

    //Get ingredient by id response dto
    public class GetIngredientByIdResponseDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string MeasurementUnit { get; set; }
        public double Amount { get; set; }
        public string Alergene { get; set; }
        public string StatusText { get; set; }
    }

    //Get íngredients by RecipeId dto
    public class GetIngredientsByRecipeIDDTO
    {
        public IEnumerable<IngredientDTO> Ingredients { get; set; }
    }

    //Delete by id response dto
    public class DeleteIngredientResponseDTO
    { 
        public string StatusText { get; set; }
    }

    //Delete multiple by Recipe response dto
    public class DeleteIngredientByRecipeIdResponseDTO
    {
        public string StatusText { get; set; }
    }

    //Create ingredient request dto
    public class CreateIngredientRequestDTO
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string MeasurementUnit { get; set; }
        public double Amount { get; set; }
        public string Alergene { get; set; }
    }

    //Create ingredient response dto
    public class CreateIngredientResponseDTO
    {
        public string Name { get; set; }
        public string StatusText { get; set; }
    }

    //Update ingredient request dto
    public class UpdateIngredientRequestDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string MeasurementUnit { get; set; }
        public double Amount { get; set; }
        public string Alergene { get; set; }
    }

    //Update ingredient response dto
    public class UpdateIngredientResponseDTO
    {
        public string Name { get; set; }
        public string StatusText { get; set; }
    }

    //Get ingredietns by search query
    public class GetIngredientsBySearchQueryResponseDTO
    { 
        public IEnumerable<IngredientDTO> Ingredients { get; set; }
    }
}
