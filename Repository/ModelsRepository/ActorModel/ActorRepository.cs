using Nero.Data;
using Nero.Models;
using Nero.Repository.IRepository;

namespace Nero.Repository.ModelsRepository.ActorModel
{
    public class ActorRepository : GenralRepository<Actor>, IActorRepository
    {
        public ActorRepository(AppDbContext context) : base(context)
        {

        }
    }
}
