using System.Collections.Generic;
using Android.Graphics;
using System.Runtime.Serialization;
using System.IO;

namespace HackMatch
{
    [DataContract]
    public class User
    {
        private Bitmap userPic;

		///	<summary>
		///	Property to get a unique username
		/// </summary>
		[DataMember]
		public string Username { get; set; }

        /// <summary>
        /// Property to get and set first names of user
        /// </summary>
		[DataMember]
        public string FirstNames { get; set; }

        /// <summary>
        /// Property to get and set last names of user
        /// </summary>
		[DataMember]
        public string LastNames { get; set; }

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
        /// Property that stores bitmap stream of ProfilePicture
        /// </summary>
		[DataMember]
        public byte[] ProfilePictureData { get; set; }

        /// <summary>
        /// Propery to access profile picture of user
        /// </summary>
        public Bitmap ProfilePicture
        {
            get
            {
                return userPic;
            }
            set
            {
                using (var stream = new MemoryStream())
                {
                    value.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    userPic = value;
                    ProfilePictureData = stream.ToArray();
                }
            }
        }
    }
}