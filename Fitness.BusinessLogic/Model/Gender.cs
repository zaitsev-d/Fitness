using System;

namespace Fitness.BusinessLogic.Model
{
    /// <summary>
    /// Gender.
    /// </summary>
    [Serializable]
    public class Gender
    {
        /// <summary>
        /// Gender Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Create new Gender.
        /// </summary>
        public Gender(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Gender name can not be null", nameof(name));
            }

            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
