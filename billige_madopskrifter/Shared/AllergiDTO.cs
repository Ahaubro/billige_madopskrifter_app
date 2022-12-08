namespace billige_madopskrifter.Shared
{
    public class AllergiDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Allergi { get; set; }
    }

    //Get allergies by user id response DTO
    public class GetAllergiesByUserIdResponseDTO
    { 
        public IEnumerable<AllergiDTO> Allergies { get; set; }
    }

    //Delete allergi response dto
    public class DeleteAllergiResponseDTO
    { 
        public string StatusText { get; set; }
    }

    //Create allergi request dto
    public class CreateAllergiRequestDTO
    {
        public int UserId { get; set; }
        public string Allergi { get; set; }
    }

    //Create allergi response dto
    public class CreateAllergiResponseDTO
    {
        public string Allergi { get; set; }
        public string StatusText { get; set; }
    }

}
