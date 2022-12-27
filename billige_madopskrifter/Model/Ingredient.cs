namespace billige_madopskrifter.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        //public Recipe Recipe { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string MeasurementUnit { get; set; }
        public double Amount { get; set; }
        public string? Alergene { get; set; }
    }
}
