using Microsoft.AspNetCore.Components.Authorization;
using Model.Entities;
using ProjektTNAI.Repository.Abstract;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor;
using static ProjektTNAI_BlazorApp.Pages.Index;

namespace ProjektTNAI_BlazorApp.Data
{
    public class CustomAdaptor : DataAdaptor
    {
        IUserActivityRepository _userActivityRepository;
        ICategoryRepository _categoryRepository;
        IActivityRepository _activityRepository;
        AuthenticationStateProvider _getAuthenticationStateAsync;
        public CustomAdaptor(IUserActivityRepository userActivityRepository, ICategoryRepository categoryRepository, IActivityRepository activityRepository, AuthenticationStateProvider authenticationStateProvider) : base()
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
            _userActivityRepository = userActivityRepository;
            _getAuthenticationStateAsync = authenticationStateProvider;
        }

        public async override Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
        {

            string userId = (await _getAuthenticationStateAsync.GetAuthenticationStateAsync()).User.Identity.Name;
            List<UserActivity> userActivities = await _userActivityRepository.GetAllUserActivitiesAsync(userId);
            List<AppointmentData> EventData = new List<AppointmentData>();
            List<Category> categories = await _categoryRepository.GetAllCategoriesAsync();
            List<Activity> activities = await _activityRepository.GetAllActivitiesAsync();
            foreach (var userActivity in userActivities)
            {
                EventData.Add(new AppointmentData()
                {
                    Id = userActivity.Id,
                    ActivityData = new ActivityDataModel()
                    {
                        Id = userActivity.Id,
                        CategoryName = categories.FirstOrDefault(x => x.Id == activities.FirstOrDefault(x => x.Id == userActivity.ActivityId).CategoryId).Name,
                        Name = activities.FirstOrDefault(x=>x.Id==userActivity.ActivityId).Name,
                    },
                    StartTime = userActivity.BeginOfActivity,
                    EndTime = userActivity.EndOfActivity,
                    Description = userActivity.Description
                }) ;
            }

            return dataManagerRequest.RequiresCounts ? new DataResult() { Result = EventData, Count = EventData.Count() } : (object)EventData;
        }
        public async override Task<object> InsertAsync(DataManager dataManager, object data, string key)
        {
            AppointmentData appointmentData = (data as AppointmentData);
            if (appointmentData.ActivityData == null || appointmentData.StartTime == null || appointmentData.EndTime == null)
                return false;
            string userName = (await _getAuthenticationStateAsync.GetAuthenticationStateAsync()).User.Identity.Name;
            var userActivity = new UserActivity()
            {
                ActivityId = appointmentData.ActivityData.Id,
                BeginOfActivity = appointmentData.StartTime,
                EndOfActivity = appointmentData.EndTime,
                UserName = userName,
                Description = appointmentData.Description==null ? "":appointmentData.Description
            };
            //appointmentData.Id = userActivity.Id;
            data = appointmentData as object;

            await _userActivityRepository.SaveUserActivityAsync(userActivity);

            return data;
        }
        public async override Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
        {
            var appointmentData = (data as AppointmentData);

            UserActivity userActivity = await _userActivityRepository.GetUserActivityAsync(appointmentData.Id);
            if (userActivity == null)
                return false;

            userActivity.ActivityId = appointmentData.ActivityData.Id;
            userActivity.BeginOfActivity = appointmentData.StartTime;
            userActivity.EndOfActivity = appointmentData.EndTime;
            userActivity.Description= appointmentData.Description;

            var result = await _userActivityRepository.SaveUserActivityAsync(userActivity);
            if (!result)
                return false;

            return data;
        }
        public async override Task<object> RemoveAsync(DataManager dataManager, object data, string keyField, string key) //triggers on appointment deletion through public method DeleteEvent
        {
            await Task.Delay(100); //To mimic asynchronous operation, we delayed this operation using Task.Delay
            int Id = (int)data;

            var result = await _userActivityRepository.DeleteUserActivityAsync(Id);
            if (!result)
                return false;
            return data;
        }
        public async override Task<object> BatchUpdateAsync(DataManager dataManager, object changedRecords, object addedRecords, object deletedRecords, string keyField, string key, int? dropIndex)
        {
            object records = deletedRecords;
            List<AppointmentData> deleteData = deletedRecords as List<AppointmentData>;
            foreach (var data in deleteData)
            {
                var result = await _userActivityRepository.DeleteUserActivityAsync(data.Id);
            }
            List<AppointmentData> addData = addedRecords as List<AppointmentData>;
            foreach (var data in addData)
            {
                string userId = (await _getAuthenticationStateAsync.GetAuthenticationStateAsync()).User.Identity.Name;
                await _userActivityRepository.SaveUserActivityAsync(new UserActivity()
                {
                    ActivityId = data.ActivityData.Id,
                    BeginOfActivity = data.StartTime,
                    EndOfActivity = data.EndTime,
                    UserName = userId,
                    Description = data.Description

                });
                records = addedRecords;
            }
            List<AppointmentData> updateData = changedRecords as List<AppointmentData>;
            foreach (var data in updateData)
            {
                var appointmentData = (data as AppointmentData);
                UserActivity userActivity = await _userActivityRepository.GetUserActivityAsync(appointmentData.Id);
                if (appointmentData != null)
                {
                    userActivity.ActivityId = appointmentData.ActivityData.Id;
                    userActivity.BeginOfActivity = appointmentData.StartTime;
                    userActivity.EndOfActivity = appointmentData.EndTime;
                    userActivity.Description = appointmentData.Description;
                }
                records = changedRecords;
            }
            return records;
        }
    }
}
