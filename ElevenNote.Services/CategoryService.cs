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

        //Create
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

        //Read
        
        //Update

        //Delete
    }
}
