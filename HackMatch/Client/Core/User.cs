﻿using System.Collections.Generic;
using Android.Graphics;
using System.Runtime.Serialization;

namespace HackMatch
{
	[DataContract]
    public class User
    {
        /// <summary>
        /// Property to get and set first names of user
        /// </summary>
		[DataMember]
        public List<string> FirstNames { get; set; }

        /// <summary>
        /// Property to get and set last names of user
        /// </summary>
		[DataMember]
        public List<string> LastNames { get; set; }

        /// <summary>
        /// Property to get and set the user biography
        /// </summary>
		[DataMember]
        public string Bio { get; set; }

        /// <summary>
        /// Property to get and set technologies (including programming languages) and their corresponding experience level
        /// </summary>
		[DataMember]
        public Dictionary<string, ExperienceLevel> Technologies { get; set; }

        /// <summary>
        /// Property to get and set spoken languages
        /// </summary>
		[DataMember]
        public List<string> SpokenLanguages { get; set; }

        /// <summary>
        /// Get the currently set profile picture of the user
        /// <para>TODO: Find out the proper type of this property</para>
        /// </summary>
		[DataMember]
        public Bitmap ProfilePicture { get; private set; } 
    }
}