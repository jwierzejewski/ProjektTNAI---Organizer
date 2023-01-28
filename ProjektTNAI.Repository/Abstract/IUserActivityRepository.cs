using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTNAI.Repository.Abstract
{
    public interface IUserActivityRepository
    {
        Task<UserActivity> GetUserActivityAsync(int id);
        Task<List<UserActivity>> GetAllUserActivitiesAsync(String UserId);
        Task<bool> SaveUserActivityAsync(UserActivity userActivity);
        Task<bool> DeleteUserActivityAsync(int id);
    }
}
