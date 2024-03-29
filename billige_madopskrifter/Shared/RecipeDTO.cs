﻿using billige_madopskrifter.Model;

namespace billige_madopskrifter.Shared
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int PrepTime { get; set; }
        public int NumberOfPersons { get; set; }
        public double EstimatedPrice { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }

    //Get all response dto
    public class GetAllRecipesResponseDTO
    { 
        public IEnumerable<RecipeDTO> Recipes { get; set; }
    }

    //Get recipe by Id response dto
    public class GetRecipeByIdResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int PrepTime { get; set; }
        public int NumberOfPersons { get; set; }
        public double EstimatedPrice { get; set; }
        public string Description { get; set; } 
        public int UserId { get; set; }
        public string StatusText { get; set; }
    }

    //Get recipes by type response dto
    public class GetRecipesByTypeResponseDTO
    {
        public IEnumerable<RecipeDTO> Recipes { get; set; }
    }

    //Get recipe by name and userId respose DTO
    public class GetByNameAndUserIdResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int PrepTime { get; set; }
        public int NumberOfPersons { get; set; }
        public double EstimatedPrice { get; set; }
        public int UserId { get; set; }
        public string StatusText { get; set; }
    }

    //Get recipes by UserId dto
    public class GetRecipesByUserIdResponseDTO
    {
        public IEnumerable<RecipeDTO> Recipes { get; set; }
    }

    //Get recipes by type and search query response dto
    public class GetRecipesByTypeAndSearchQueryResponseDTO
    {
        public IEnumerable<RecipeDTO> Recipes { get; set; }
    }

    //Delete recipe response dto
    public class DeleteRecipeReponseDTO
    { 
        public string StatusText { get; set; }
    }

    // Create request dto
    public class CreateRecipeRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int PrepTime { get; set; }
        public int NumberOfPersons { get; set; }
        public double EstimatedPrice { get; set; }
        public int UserId { get; set; }
    }

    //Create response dto
    public class CreateRecipeResponseDTO
    {
        public string Name { get; set; }
        public string StatusText { get; set; }

    }

    //Update recipe request dto
    public class UpdateRecipeRequestDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int PrepTime { get; set; }
        public string Description { get; set; }
        public int NumberOfPersons { get; set; }
        public double EstimatedPrice { get; set; }
        public int UserId { get; set; }
    }

    //Update recipe response dto
    public class UpdateRecipeResponseDTO
    {
        public string Name { get; set; }
        public string StatusText { get; set; }
    }

    //Update recipe description request DTO
    public class UpdateDescriptionRequestDTO
    {
        public string Description { get; set; }
    }

    //Update description only ResponsDTO
    public class UpdateDescriptionResponseDTO
    {
        public string Name { get; set; }
        public string StatusText { get; set; }
    }


}
