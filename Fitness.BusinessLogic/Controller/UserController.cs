using Fitness.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fitness.BusinessLogic.Controller
{
    public class UserController : ControllerBase
    {
        private const string USER_FILE_NAME = "Users.dat";
        public List<User> Users { get; }
        public User CurrentUser { get; }
        public bool IsNewUser { get; } = false;

        /// <summary>
        /// User application.
        /// </summary>
        public UserController(string userName)
        {
            if(string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("User Name can not be null.", nameof(userName));
            }

            Users = GetUsersData();
            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);
            if(CurrentUser == null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                IsNewUser = true;
                Save();
            }
        }

        /// <summary>
        /// Saving user data.
        /// </summary>
        private void Save()
        {
            Save(USER_FILE_NAME, Users);
        }

        /// <summary>
        /// Get saved Users list.
        /// </summary>
        /// <returns></returns>
        private List<User> GetUsersData()
        {
           return Load<List<User>>(USER_FILE_NAME) ?? new List<User>();
        }

        public void SetNewUserData(string genderName, DateTime birthDate, double weight = 1, double height = 1)
        {
            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birthDate;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            Save();
        }
    }
}

