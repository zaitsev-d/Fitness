using Fitness.BusinessLogic.Controller;
using Fitness.BusinessLogic.Model;
using System;
using System.Globalization;
using System.Resources;

namespace Fitness.CMD
{
    class Program
    {
        private static string cultureData = default, resourceManagerData = default;

        static void Main(string[] args)
        {
            ChooseLanguage();

            var culture = CultureInfo.CreateSpecificCulture(cultureData);
            var resourceManager = new ResourceManager(resourceManagerData, typeof(Program).Assembly);

            Console.WriteLine(resourceManager.GetString("Hello", culture));

            Console.Write(resourceManager.GetString("Username", culture));
            var name = Console.ReadLine();

            var userController = new UserController(name);
            var eatingController = new EatingController(userController.CurrentUser);
            var exerciseController = new ExerciseController(userController.CurrentUser);
            if (userController.IsNewUser)
            {
                Console.Write(resourceManager.GetString("Gender", culture));
                var gender = Console.ReadLine();
                var birthDate = ParseDateTime(resourceManager.GetString("Birthday", culture));
                double weight = ParseDouble(resourceManager.GetString("Weight", culture));
                double height = ParseDouble(resourceManager.GetString("Height", culture));

                userController.SetNewUserData(gender, birthDate, weight, height);
            }

            Console.WriteLine(userController.CurrentUser);

            Console.WriteLine("-----------------------------");
            Console.WriteLine(resourceManager.GetString("Menu", culture));
            Console.WriteLine(resourceManager.GetString("FoodIntake", culture));
            Console.WriteLine(resourceManager.GetString("ExerciseIntroduce", culture));
            Console.WriteLine(resourceManager.GetString("Quit", culture));

            while(true)
            {
                Console.WriteLine("-----------------------------");
                Console.Write(resourceManager.GetString("ToDo", culture));
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.E:
                        var foods = EnterEating();
                        eatingController.Add(foods.Item1, foods.Item2);
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("Food List: ");
                        foreach (var item in eatingController.Eating.Foods)
                        {
                            Console.WriteLine($"\t{item.Key} - {item.Value}");
                        }
                        break;

                    case ConsoleKey.A:
                        var exercise = EnterExercise();
                        exerciseController.Add(exercise.Item1, exercise.Item2, exercise.Item3);
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("Exercises List: ");
                        foreach (var item in exerciseController.Exercises)
                        {
                            Console.WriteLine($"\t{item.Activity} with {item.Start.ToShortTimeString()} before {item.Finish.ToShortTimeString()}.");
                        }
                        break;

                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }

                Console.WriteLine("-----------------------------");
                Console.Write(resourceManager.GetString("Enter", culture));
                Console.ReadLine();
            }
        }

        private static Tuple<DateTime, DateTime, Activity> EnterExercise()
        {
            var culture = CultureInfo.CreateSpecificCulture("EN-EN");
            var resourceManager = new ResourceManager("Fitness.CMD.Languages.Messages-EN-EN", typeof(Program).Assembly);

            Console.Write(resourceManager.GetString("ExerciseName", culture));
            var name = Console.ReadLine();

            var energy = ParseDouble(resourceManager.GetString("Energy", culture));
            var begin = ParseDateTime(resourceManager.GetString("StartExercise", culture));
            var end = ParseDateTime(resourceManager.GetString("EndingExercise", culture));

            var activity = new Activity(name, energy);
            return Tuple.Create(begin, end, activity);
        }

        private static Tuple<Food, double> EnterEating()
        {
            var culture = CultureInfo.CreateSpecificCulture("EN-EN");
            var resourceManager = new ResourceManager("Fitness.CMD.Languages.Messages-EN-EN", typeof(Program).Assembly);

            Console.Write(resourceManager.GetString("ProductName", culture));
            var food = Console.ReadLine();

            var calories = ParseDouble(resourceManager.GetString("Calories", culture));
            var proteins = ParseDouble(resourceManager.GetString("Proteins", culture));
            var fats = ParseDouble(resourceManager.GetString("Fats", culture));
            var carbohydrates = ParseDouble(resourceManager.GetString("Carbohydrates", culture));
            var weight = ParseDouble(resourceManager.GetString("WeightPort", culture));

            var product = new Food(food, calories, proteins, fats, carbohydrates);

            return Tuple.Create(product, weight);
        }

        private static DateTime ParseDateTime(string value)
        {
            DateTime birthDate;
            while (true)
            {
                Console.Write($"Enter your {value} (dd.mm.yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Invalid {value} format.");
                }
            }

            return birthDate;
        }

        private static double ParseDouble(string name)
        {
            while (true)
            {
                Console.Write($"Enter {name}: ");
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"Invalid {name} fields.");
                }
            }
        }

        static void ChooseLanguage()
        {
            string language = default;
            Console.WriteLine("--- L A N G U A G E ---");
            Console.WriteLine("EN - ENGLISH");
            Console.WriteLine("RUS - RUSSIAN");
            Console.WriteLine("-----------------------------");
            Console.Write("Choose language: ");
            language = Console.ReadLine();

            switch (language)
            {
                case "EN":
                    cultureData = "EN-EN";
                    resourceManagerData = "Fitness.CMD.Languages.Messages-EN-EN";
                    break;

                case "RUS":
                    cultureData = "RU-RU";
                    resourceManagerData = "Fitness.CMD.Languages.Messages-RU-RU";
                    break;
            }
            Console.Clear();
        }
    }
}
