using System;

namespace Fitness.BusinessLogic.Model
{
    [Serializable]
    public class Food
    {
        public string Name { get; }

        /// <summary>
        /// Properties
        /// </summary>
        public double Proteins { get; }
        public double Fats { get; }
        public double Carbohydrates { get; }

        /// <summary>
        /// Calories for 100 gram products
        /// </summary>
        public double Calories { get; }

        private double ColoriesOneGram { get { return Calories / 100.0; } }
        private double ProteinsOneGram { get { return Proteins / 100.0; } }
        private double FatsOneGram { get { return Fats / 100.0; } }
        private double CarbohydratesOneGram { get { return Carbohydrates / 100.0; } }

        public Food(string name) : this(name, 0, 0, 0, 0) {}

        public Food(string name, double calories, double proteins, double fats, double carbohydrates)
        {
            Name = name;
            Calories = calories / 100.0;
            Proteins = proteins / 100.0;
            Fats = fats / 100.0;
            Carbohydrates = carbohydrates / 100.0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
