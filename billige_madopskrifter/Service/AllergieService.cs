using billige_madopskrifter.Data;
using billige_madopskrifter.Model;
using billige_madopskrifter.Shared;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace billige_madopskrifter.Service
{
    public interface IAllergieService
    {
        Task<CreateAllergiResponseDTO> CreateAllergi(CreateAllergiRequestDTO dto);
        Task<GetAllergiesByUserIdResponseDTO> GetByUserId(int userId);
        Task<DeleteAllergiResponseDTO> DeleteAllergi(int id);
    }
    public class AllergieService : IAllergieService
    {

        private readonly DBContext _dbContext;

        //Constructor
        public AllergieService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Create new allergi
        public async Task<CreateAllergiResponseDTO> CreateAllergi(CreateAllergiRequestDTO dto)
        {

            var check = _dbContext.Allergies.AsNoTracking().FirstOrDefault(a => a.UserId == dto.UserId && a.Allergi == dto.Allergi);

            if (check != null)
            {
                return new CreateAllergiResponseDTO
                {
                    StatusText = "You allready added an allergi by the name",
                    Allergi = dto.Allergi
                };           
            }

            var entity = _dbContext.Allergies.Add(new Allergies
            {
                UserId = dto.UserId,
                Allergi = dto.Allergi
            });

            await _dbContext.SaveChangesAsync();

            return new CreateAllergiResponseDTO
            {
                Allergi = entity.Entity.Allergi,
                StatusText = "New allergi created"
            };
        }

        //Get allergies by userId
        public async Task<GetAllergiesByUserIdResponseDTO> GetByUserId(int userId)
        {
            var allergies = _dbContext.Allergies.AsNoTracking().Where(a => a.UserId == userId);

            if (allergies != null)
            {
                return new GetAllergiesByUserIdResponseDTO
                {
                    Allergies = allergies.Select(a => new AllergiDTO
                    {
                        Id = a.Id,
                        UserId = a.UserId,
                        Allergi = a.Allergi
                    })
                };
            }

            return null;
        }

        //Delete allergi
        public async Task<DeleteAllergiResponseDTO> DeleteAllergi(int id)
        {
            var allergi = _dbContext.Allergies.FirstOrDefault(a => a.Id == id);

            if (allergi != null)
            {
                _dbContext.Allergies.Remove(allergi);
                await _dbContext.SaveChangesAsync();

                return new DeleteAllergiResponseDTO
                {
                    StatusText = "Allergi deleted"
                };
            }

            return new DeleteAllergiResponseDTO
            {
                StatusText = "Error, no allergi found"
        };
        }

    }
}
