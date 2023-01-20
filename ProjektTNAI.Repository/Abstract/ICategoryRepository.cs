using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTNAI.Repository.Abstract
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<bool> SaveCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
