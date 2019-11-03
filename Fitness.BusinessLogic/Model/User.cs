using System;

namespace Fitness.BusinessLogic.Model
{
    /// <summary>
    /// User.
    /// </summary>
    [Serializable]
    public class User
    {
        #region Properties
        public string Name { get; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        //DateTime nowDate = DateTime.Today;
        //int age = nowDate.Year - birthDate.Year;
        //if(birthDate > nowDate.AddYears(-age)) age--;
        public int Age { get { return DateTime.Now.Year - BirthDate.Year; } }
        #endregion

        public User(string name, Gender gender, DateTime birthDate, double weight, double height)
        {
            #region Exceptions
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("User name can not be null.", nameof(name));
            }

            if (gender == null)
            {
                throw new ArgumentNullException("Gender can not be null.", nameof(gender));
            }

            if (birthDate < DateTime.Parse("01.01.1900") || birthDate >= DateTime.Now)
            {
                throw new ArgumentException("Uncorrect birth date.", nameof(birthDate));
            }

            if (weight <= 0)
            {
                throw new ArgumentException("Weight can not be 0.", nameof(weight));
            }

            if (height <= 0)
            {
                throw new ArgumentNullException("Height can not be 0.", nameof(height));
            }
            #endregion

            Name = name;
            Gender = gender;
            BirthDate = birthDate;
            Weight = weight;
            Height = height;
        }

        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("User name can not be null.", nameof(name));
            }

            Name = name;
        }

        public override string ToString()
        {
            return Name + " " + Age;
        }
    }
}
