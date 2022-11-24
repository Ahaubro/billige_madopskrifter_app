using System.Text.Json.Serialization;

namespace billige_madopskrifter.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        //[JsonIgnore]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public IList<Recipe>? Recipes { get; set; }
        //public IList<Review>? Reviews { get; set; }
    }
}
