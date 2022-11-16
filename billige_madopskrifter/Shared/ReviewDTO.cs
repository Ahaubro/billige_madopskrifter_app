namespace billige_madopskrifter.Shared
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }

    //Get reviews by RecipeId dto
    public class GetReviewsByUserIDResponseDTO
    {
        public IEnumerable<ReviewDTO> Reviews { get; set; }
    }

    //Get all reviews
    public class GetAllReviewsDTOReponseDTO
    {
        public IEnumerable<ReviewDTO> Reviews { get; set; }
    }

    //Create review request dto
    public class CreateReviewRequestDTO
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }

    //Create ingredient response dto
    public class CreateReviewResponseDTO
    {
        public string StatusText { get; set; }
    }

    //Delete review response dto
    public class DeleteReviewResponseDTO
    {
        public string StatusText { get; set; }
    }
}
