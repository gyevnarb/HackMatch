using System;
using System.Linq;
using System.IO;
using System.Xml;
using Android.Graphics;
using Android.Content.Res;

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

        /// <summary>
        /// Loads all hackathons' data from Assets/Hackathons
        /// <para>Supply Assets property within onCreate() method to run this function</para>
        /// </summary>
        /// <param name="assets">AssetManager to read from Assets folder</param>
        public void LoadHackathons(AssetManager assets)
        {
            string[] files = Directory.GetFiles("Assets/Hackathons", ".*xml");
            
            foreach (string name in files)
            {
                XmlReader reader = assets.OpenXmlResourceParser(name);
                
            }
        }
    }
}