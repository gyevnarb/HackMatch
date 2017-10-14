using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using HackMatch.Resources;

namespace HackMatch
{
    [Activity(Label = "HackMatch", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);  

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            EditText editText1 = FindViewById<EditText>(Resource.Id.editText1);
            TextView textView2 = FindViewById<TextView>(Resource.Id.txtThis);
            editText1.Text = "Yay, edit works!";
            editText1.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {

                textView2.Text = e.Text.ToString(); 
            
            };
            Button authenticateButton = FindViewById<Button>(Resource.Id.authenticateButton);
            var intent = new Intent(this, typeof(EventsPageActivity));
            StartActivity(intent);

        }
    }
}

