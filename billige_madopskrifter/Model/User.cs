using System.Text.Json.Serialization;

namespace billige_madopskrifter.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        //Foreign keys
        public IList<Recipe>? Recipes { get; set; }
        public IList<Review>? Reviews { get; set; }
        public IList<LikedRecipes>? LikedRecipes { get; set; }
        public IList<Allergies>? Allergies { get; set; }
    }
}
