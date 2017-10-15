using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using Android.Graphics;

namespace HackMatch
{
    [Activity(Label = "ProfilePageActivity")]
    public class ProfilePageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ProfilePage);

            Button matchingView = FindViewById<Button>(Resource.Id.matchingViewButton);
            matchingView.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ProfilePageActivity));
                StartActivity(intent);
            };

            Button eventsView = FindViewById<Button>(Resource.Id.eventsViewButton);
            eventsView.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ProfilePageActivity));
                StartActivity(intent);
            };
            Button contactsView = FindViewById<Button>(Resource.Id.contactsViewButton);
            contactsView.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ContactsPageActivity));
                StartActivity(intent);
            };

            User u = new User();
            u.FirstNames = "Balint";
            u.LastNames = "Gyevnar";
            u.Username = "gyevnarb";
            u.SpokenLanguages = new List<string>{ "English", "Hungarian", "German", "Russian"};
            u.Technologies = new Dictionary<string, ExperienceLevel> {
                { "C#", ExperienceLevel.Experienced},
                { "Git", ExperienceLevel.Experienced },
                { "Windows", ExperienceLevel.Professional},
                { "Tensorflow", ExperienceLevel.Practiced}
            };
            u.Bio = "Hi! I am 20 year Hungarian coder, who has a great passion for machine learning and sustainable development. I also like to play the piano and learn new languages.";
            u.SetProfilePicture(BitmapFactory.DecodeStream(Resources.OpenRawResource(Resource.Drawable.me)));
            try
            {
                IServerCommunicator com = new ServerConnection(Constants.SERVER, Constants.PORT);
				Console.Out.WriteLine("Creating profile...");
                com.CreateProfile(u);
				Console.Out.WriteLine("Profile created?");
			}
            catch (Exception ex)
            {
                Log.Error(Constants.LOG_TAG, ex.Message);
            }

            TextView fname = FindViewById<TextView>(Resource.Id.fnameTextView);
            fname.Text = u.FirstNames;
            TextView lname = FindViewById<TextView>(Resource.Id.lnameTextView);
            lname.Text = u.LastNames;
            TextView uname = FindViewById<TextView>(Resource.Id.unameTextView);
            uname.Text = u.Username;
            TextView bio = FindViewById<TextView>(Resource.Id.bioTextView);
            bio.Text = u.Bio;
            TextView techs = FindViewById<TextView>(Resource.Id.techTextView);
            techs.Text = Utils.DictionaryToString<string, ExperienceLevel>(u.Technologies);
            TextView natLangs = FindViewById<TextView>(Resource.Id.natLangTextView);
            natLangs.Text = u.SpokenLanguages.Aggregate((str, s) => str += $"• {s}\n");
        }
    }
}