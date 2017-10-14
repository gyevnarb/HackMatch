using System;
using Android.Graphics;

namespace HackMatch
{
    public class Hackathon
    {
        /// <summary>
        /// Gets name of the hackathon
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets start date of the hackathon
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets end date of the hackathon
        /// </summary>
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// Get location of the hackathon
        /// </summary>
        public string Location { get; private set; }

        /// <summary>
        /// Gets description of the hackathon
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets background image associated with the hackathon
        /// <para>TODO: Find out proper type of the property</para>
        /// </summary>
        public Bitmap Background { get; private set; }
    }
}