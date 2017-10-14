using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Globalization;
using Android.Graphics;
using Android.Util;
using Android.App;

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
        public static List<Hackathon> LoadHackathons(XmlReader reader)
        {
            List<Hackathon> hacks = new List<Hackathon>();
            reader.Read(); //Skip to first hackathon
            Hackathon current = new Hackathon(); ;
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "hackathon":
                            if (current.Name != null) hacks.Add(current);
                            current = new Hackathon();
                            break;
                        case "name":
                            reader.Read();
                            current.Name = reader.Value;
                            reader.Read();
                            break;
                        case "start":
                            reader.Read();                            
                            current.StartDate = DateTime.ParseExact(reader.Value, Constants.DATETIME_FORMAT, provider);
                            reader.Read();
                            break;
                        case "end":
                            reader.Read();
                            current.EndDate = DateTime.ParseExact(reader.Value, Constants.DATETIME_FORMAT, provider);
                            reader.Read();
                            break;
                        case "loc":
                            reader.Read();
                            current.Location = reader.Value;
                            reader.Read();
                            break;
                        case "desc":
                            reader.Read();
                            current.Description = reader.Value;
                            reader.Read();
                            break;
                        case "img":
                            reader.Read();
                            Stream bmp = Application.Context.Assets.Open(reader.Value);
                            current.Background = BitmapFactory.DecodeStream(bmp);
                            reader.Read();
                            break;
                    }
                }
            }
            catch (Java.IO.IOException ex)
            {
                Log.Error(Constants.LOG_TAG, ex.Message);
            }

            return hacks;
        }

        public override string ToString()
        {
            return $"Name: {Name}\n From: {StartDate} till {EndDate}\n In: {Location}\n Description: {Description}";
        }
    }
}