using billige_madopskrifter.Service;
using billige_madopskrifter.Shared;
using Microsoft.AspNetCore.Mvc;

namespace billige_madopskrifter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AllergiController
    {

        private readonly IAllergieService _allergiService;

        //Constructor
        public AllergiController(IAllergieService allergieService)
        {
            _allergiService = allergieService;
        }

        //Get allergies by userId
        [Produces("application/json")]
        [HttpGet("{userId:int}")]
        public async Task<GetAllergiesByUserIdResponseDTO> GetByUserId(int userId)
        {
            return await _allergiService.GetByUserId(userId);
        }

        //Create allergi
        [Produces("application/json")]
        [HttpPost]
        public async Task<CreateAllergiResponseDTO> CreateAllergi(CreateAllergiRequestDTO dto)
        {
            return await _allergiService.CreateAllergi(dto);
        }

        //Delete allergi
        [Produces("application/json")]
        [HttpDelete("{id:int}")]
        public async Task<DeleteAllergiResponseDTO> DeleteAllergi(int id)
        {
            return await _allergiService.DeleteAllergi(id);
        }
    }
}
