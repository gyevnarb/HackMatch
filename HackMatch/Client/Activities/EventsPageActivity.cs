﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HackMatch.Resources
{
    [Activity(Label = "EventsPageActivity")]
    public class EventsPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.EventsPage);
            
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
           
            Button contactsView = FindViewById<Button>(Resource.Id.contactsViewButton);
            contactsView.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ContactsPageActivity));
                StartActivity(intent);
            };



            //upc id = 1, junction = 2
            ImageButton img1 = FindViewById<ImageButton>(Resource.Id.imageButton1);
            img1.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MatchingPageActivity));
                MatchingPageActivity.hackathonId = 1;
                StartActivity(intent);
            };
            ImageButton img2 = FindViewById<ImageButton>(Resource.Id.imageButton2);
            img2.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MatchingPageActivity));
                MatchingPageActivity.hackathonId = 2;
                StartActivity(intent);
            };
            ImageButton img3 = FindViewById<ImageButton>(Resource.Id.imageButton3);
            img3.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MatchingPageActivity));
                MatchingPageActivity.hackathonId = 3;
                StartActivity(intent);
            };


            var layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;

            var aLabel = new TextView(this);
            aLabel.Text = "Hello, World!!!";

            var aButton = new Button(this);
            aButton.Text = "Say Hello!";

            aButton.Click += (sender, e) =>
            { aLabel.Text = "Hello Android!"; };

            layout.AddView(aLabel);
            layout.AddView(aButton);
            
            //SetContentView(layout);

        }
    }
}