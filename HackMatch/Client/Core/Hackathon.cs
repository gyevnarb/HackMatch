using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using Android.Content.Res;
using Android.Graphics;

namespace HackMatch
{
    public class Hackathon
    {

        /// <summary>
        /// Gets name of the hackathon
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets start date of the hackathon
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets end date of the hackathon
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Get location of the hackathon
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets description of the hackathon
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets background image associated with the hackathon
        /// <para>TODO: Find out proper type of the property</para>
        /// </summary>
        public Bitmap Background { get; set; }

        /// <summary>
        /// Loads all hackathons' data from Assets/Hackathons
        /// <para>Supply Assets property within onCreate() method to run this function</para>
        /// </summary>
        /// <param name="assets">AssetManager to read from Assets folder</param>
        public static List<Hackathon> LoadHackathons(AssetManager assets)
        {
            List<Hackathon> hacks = new List<Hackathon>();
            Hackathon current = new Hackathon(); ;

            XmlReader reader = assets.OpenXmlResourceParser(Constants.HACKATHONS_XML);
            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "hackathon":
                        current = new Hackathon();
                        break;
                    case "name":
                        current.Name = reader.Value;
                        break;
                    case "start":
                        current.StartDate = DateTime.Parse(reader.Value);
                        break;
                    case "end":
                        current.EndDate = DateTime.Parse(reader.Value);
                        break;
                    case "loc":
                        current.Location = reader.Value;
                        break;
                    case "desc":
                        current.Description = reader.Value;
                        break;
                    case "img":
                        Stream bmp = assets.Open(reader.Value);
                        current.Background = BitmapFactory.DecodeStream(bmp);
                        break;
                    default:
                        hacks.Add(current);
                        break;
                }
            }

            return hacks;
        }
    }
}