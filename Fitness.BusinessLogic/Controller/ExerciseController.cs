using Fitness.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fitness.BusinessLogic.Controller
{
    public class ExerciseController : ControllerBase
    {
        private const string EXERSICES_FILE_NAME = "Exercises.dat";
        private const string ACTIVITIES_FILE_NAME = "Activities.dat";
        private User user;
        public List<Exercise> Exercises { get; }
        public List<Activity> Activities { get; }

        public ExerciseController(User user)
        {
            this.user = user ?? throw new ArgumentNullException(nameof(user));
            Exercises = GetAllExercises();
            Activities = GetAllActivities();
        }

        public void Add(DateTime begin, DateTime end, Activity activity)
        {
            var act = Activities.SingleOrDefault(a => a.Name == activity.Name);
            if(act == null)
            {
                Activities.Add(activity);

                var exercise = new Exercise(begin, end, activity, user);
                Exercises.Add(exercise);
            }
            else
            {
                var exercise = new Exercise(begin, end, act, user);
                Exercises.Add(exercise);
            }

            Save();
        }

        private List<Exercise> GetAllExercises()
        {
            return Load<List<Exercise>>(EXERSICES_FILE_NAME) ?? new List<Exercise>();
        }

        private List<Activity> GetAllActivities()
        {
            return Load<List<Activity>>(ACTIVITIES_FILE_NAME) ?? new List<Activity>();
        }

        private void Save()
        {
            Save(EXERSICES_FILE_NAME, Exercises);
            Save(ACTIVITIES_FILE_NAME, Activities);
        }
    }
}
