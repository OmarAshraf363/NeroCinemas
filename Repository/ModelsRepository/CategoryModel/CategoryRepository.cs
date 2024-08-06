using Microsoft.EntityFrameworkCore;
using Nero.Data;
using Nero.Models;
using Nero.Repository.IRepository;

namespace Nero.Repository.ModelsRepository.CategoryModel
{
    public class CategoryRepository : GenralRepository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
      
    }
}
