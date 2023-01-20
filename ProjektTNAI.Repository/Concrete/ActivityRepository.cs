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
    public class ActivityRepository : BaseRepository, IActivityRepository
    {
        public ActivityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Activity> GetActivityAsync(int id)
        {
            return await Context.Activities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Activity>> GetAllActivitiesAsync()
        {
            return await Context.Activities.ToListAsync();
        }

        public async Task<bool> SaveActivityAsync(Activity activity)
        {
            if (activity == null)
                return false;

            Context.Entry(activity).State = activity.Id == default(int) ? EntityState.Added : EntityState.Modified;

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

        public async Task<bool> DeleteActivityAsync(int id)
        {
            var activity = await GetActivityAsync(id);
            if (activity == null)
                return true;

            Context.Activities.Remove(activity);

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
