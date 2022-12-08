namespace billige_madopskrifter.Shared
{
    public class LikedRecipeDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
    }

    // Create liked recipe request DTO
    public class CreateLikedRecipeRequestDTO
    { 
        public int UserId { get; set; }
        public int RecipeId { get; set; }
    }

    //Create liked recipe reesonse DTO
    public class CreateLikedRecipeResponseDTO
    { 
        public string StatusText { get; set; }
    }

    //Get multiple liked recipes by userId
    public class GetLikedRecipesByUserIdResponseDTO
    {
        public IEnumerable<RecipeDTO> Recipes { get; set; }
    }

    //Get likedrecipes by userId and recipeId responseDTO
    public class GetLikedRecipeByUserIdAndRecipeIdResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string StatusText { get; set; }
    }

}
