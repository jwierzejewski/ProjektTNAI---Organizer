using Microsoft.EntityFrameworkCore;
using Model.Entities;
using ProjektTNAI.Model;
using ProjektTNAI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTNAI.Repository.Concrete
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository (AppDbContext context):base(context)
        { }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await Context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            
            return await Context.Categories.ToListAsync();
        }

        public async Task<bool> SaveCategoryAsync(Category category)
        {
            if (category == null)
                return false;

            Context.Entry(category).State = category.Id == default(int) ? EntityState.Added : EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryAsync(id);
            if (category == null)
                return true;

            Context.Categories.Remove(category);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
