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
    [Activity(Label = "ContactsPageActivity")]
    public class ContactsPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ContactsPage);

            Button profileView = FindViewById<Button>(Resource.Id.profileViewButton);
            profileView.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ProfilePageActivity));
                StartActivity(intent);
            };

            Button matchingView = FindViewById<Button>(Resource.Id.matchingViewButton);
            matchingView.Click += (sender, e) =>
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
            
            // Create your application here
        }
    }
}