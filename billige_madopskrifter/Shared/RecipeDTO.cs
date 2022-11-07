namespace billige_madopskrifter.Shared
{
    //Recipe dto class
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int PrepTime { get; set; }
        public int NumberOfPersons { get; set; }
        public int UserId { get; set; }
    }

    //Get all response
    public class GetAllRecipesResponseDTO
    { 
        public IEnumerable<RecipeDTO> Recipes { get; set; }
    }


    //Get recipe by Id response
    public class GetRecipeByIdResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int PrepTime { get; set; }
        public int NumberOfPersons { get; set; }
        public int UserId { get; set; }
        public string StatusText { get; set; }
    }


    //Get recipes by UserId
    public class GetRecipesByUserIdResponseDTO
    {
        public IEnumerable<RecipeDTO> Recipes { get; set; }
    }


    //Delete recipe response
    public class DeleteRecipeReponseDTO
    { 
        public string StatusText { get; set; }
    }


    // Create request
    public class CreateRecipeRequestDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int PrepTime { get; set; }
        public int NumberOfPersons { get; set; }
        public int UserId { get; set; }
    }


    //Create response
    public class CreateRecipeResponseDTO
    {
        public string Name { get; set; }
        public string StatusText { get; set; }

    }


    //Update recipe request 
    public class UpdateRecipeRequestDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int PrepTime { get; set; }
        public int NumberOfPersons { get; set; }
        public int UserId { get; set; }
    }


    //Update recipe response
    public class UpdateRecipeResponseDTO
    { 
        public string Name { get; set; }
        public string StatusText { get; set; }
    }


}
