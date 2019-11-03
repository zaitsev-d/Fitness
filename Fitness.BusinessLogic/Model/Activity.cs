using System;

namespace Fitness.BusinessLogic.Model
{
    [Serializable]
    public class Activity
    {
        public string Name { get; }
        public double CaloriesPerMinute { get; }

        public Activity(string name, double caloriesPerMinute)
        {
            Name = name;
            CaloriesPerMinute = caloriesPerMinute;
        }

        public override string ToString()
        {
            return  Name;
        }
    }
}
