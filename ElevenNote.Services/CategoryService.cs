using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;
        
        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCategory(CategoryCreate category)
        {
            var entity = new Category
            {
                OwnerId = _userId,
                Name = category.Name
            };

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        
        public CategoryListItem GetCategoryById(int id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories
                    .FirstOrDefault(c => c.OwnerId == _userId && c.CategoryId == id);

                return new CategoryListItem
                {
                    CategoryId = entity.CategoryId,
                    Name = entity.Name
                };
            }
        }
        public IEnumerable<CategoryListItem> GetAllCategories()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var query = ctx.Categories
                   .Where(c => c.OwnerId == _userId)
                   .Select(c => new CategoryListItem
                   {
                       CategoryId = c.CategoryId,
                       Name = c.Name
                   });  
                 
                return query.ToArray();
            }
        }
        
        public bool UpdateCategory(CategoryEdit category)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Categories
                       .FirstOrDefault(c => c.OwnerId == _userId && c.CategoryId == category.CategoryId);

                entity.Name = category.Name;

                return ctx.SaveChanges() == 1;
            }
        }
        
        public bool DeleteCategoryById(int id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories
                    .FirstOrDefault(c => c.OwnerId == _userId && c.CategoryId == id);

                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
