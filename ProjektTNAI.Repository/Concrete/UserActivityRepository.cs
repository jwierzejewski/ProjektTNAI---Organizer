using Microsoft.EntityFrameworkCore;
using Model.Entities;
using ProjektTNAI.Model;
using ProjektTNAI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTNAI.Repository.Concrete
{
    public class UserActivityRepository : BaseRepository, IUserActivityRepository
    {

        public UserActivityRepository(AppDbContext context) : base(context) { }

        public async Task<UserActivity> GetUserActivityAsync(int id)
        {
            return await Context.UserActivities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UserActivity>> GetAllUserActivitiesAsync(string UserName)
        {         
            return await Context.UserActivities.Where(x => x.UserName == UserName).ToListAsync();

        }

        public async Task<bool> SaveUserActivityAsync(UserActivity userActivity)
        {
            if (userActivity == null)
                return false;

            Context.Entry(userActivity).State = userActivity.Id == default(int) ? EntityState.Added : EntityState.Modified;

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

        public async Task<bool> DeleteUserActivityAsync(int id)
        {
            var userActivity = await GetUserActivityAsync(id);
            if (userActivity == null)
                return true;

            Context.UserActivities.Remove(userActivity);

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
