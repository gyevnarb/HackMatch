using System.Collections.Generic;

namespace HackMatch
{
    public class User
    {
        /// <summary>
        /// Property to get and set first names of user
        /// </summary>
        public List<string> FirstNames { get; set; }

        /// <summary>
        /// Property to get and set last names of user
        /// </summary>
        public List<string> LastNames { get; set; }

        /// <summary>
        /// Property to get and set the user biography
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Property to get and set technologies (including programming languages) and their corresponding experience level
        /// </summary>
        public Dictionary<string, ExperienceLevel> Technologies { get; set; }

        /// <summary>
        /// Property to get and set spoken languages
        /// </summary>
        public List<string> SpokenLanguages { get; set; }
    }
}