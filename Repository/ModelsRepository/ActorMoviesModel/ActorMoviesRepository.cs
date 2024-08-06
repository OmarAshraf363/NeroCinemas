﻿

using Nero.Data;
using Nero.Models;
using Nero.Repository.IRepository;

namespace Nero.Repository.ModelsRepository.ActorMoviesModel
{
    public class ActorMoviesRepository : GenralRepository<ActorMovie>
    {
        public ActorMoviesRepository(AppDbContext context) : base(context)
        {
        }
    }
}
