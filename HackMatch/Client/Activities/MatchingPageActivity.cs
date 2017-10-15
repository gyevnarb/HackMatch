using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HackMatch.Resources;

namespace HackMatch
{
    [Activity(Label = "MatchingPageActivity")]
    public class MatchingPageActivity : Activity
    {
        public static int hackathonId;   
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MatchingPage);

            Button profileView = FindViewById<Button>(Resource.Id.profileViewButton);
            profileView.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ProfilePageActivity));
                StartActivity(intent);
            };

           

            Button eventsView = FindViewById<Button>(Resource.Id.eventsViewButton);
            eventsView.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(EventsPageActivity));
                StartActivity(intent);
            };
            Button contactsView = FindViewById<Button>(Resource.Id.contactsViewButton);
            contactsView.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ContactsPageActivity));
                StartActivity(intent);
            };
            // Create your application here
        }
    }
}