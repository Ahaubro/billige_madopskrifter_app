using Microsoft.EntityFrameworkCore;

namespace billige_madopskrifter.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int PrepTime { get; set; }
        public int NumberOfPersons { get; set; }
        public double EstimatedPrice { get; set; }
        public IList<Ingredient>? Ingredients { get; set; }
        public IList<Review>? Reviews { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }

    }

}
