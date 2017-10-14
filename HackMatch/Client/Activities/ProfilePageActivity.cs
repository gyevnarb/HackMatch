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
        }
    }
}