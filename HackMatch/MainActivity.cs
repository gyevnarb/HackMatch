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
            
            EditText username = FindViewById<EditText>(Resource.Id.userName);
            EditText password = FindViewById<EditText>(Resource.Id.passWord);
            //editText1.Text = "Yay, edit works!";
            //editText1.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {

            //    textView2.Text = e.Text.ToString(); 
            
            //};

            Button authenticateButton = FindViewById<Button>(Resource.Id.authenticateButton);
            authenticateButton.Click += (sender, e) =>
            {
                string un = username.Text;
                string pw = password.Text;
                if (checkCredentials(un, pw))
                {
                    var intent = new Intent(this, typeof(EventsPageActivity));
                    StartActivity(intent);
                }
            };

            //Load hackathons
            List<Hackathon> hacks;
            using (XmlReader reader = Resources.GetXml(Resource.Xml.Hackathons))
            {
                hacks = Hackathon.LoadHackathons(reader);
            }
        }

        private bool checkCredentials(string username, string password)
        {
            //TODO: Implement secure login
            return true;//username == "root" && password == "login";
        }
    }
}

