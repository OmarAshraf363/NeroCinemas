using Nero.Data;
using Nero.Models;
using Nero.Repository.IRepository;

namespace Nero.Repository.ModelsRepository.CinemaModel
{
    public class CinemaRepository : GenralRepository<Cinema>
    {
        public CinemaRepository(AppDbContext context) : base(context) { }
       
    }
}
