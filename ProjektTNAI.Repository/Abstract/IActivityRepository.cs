using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTNAI.Repository.Abstract
{
    public interface IActivityRepository
    {
        Task<Activity> GetActivityAsync(int id);
        Task<List<Activity>> GetAllActivitiesAsync();
        Task<bool> SaveActivityAsync(Activity activity);
        Task<bool> DeleteActivityAsync(int id);
    }
}
