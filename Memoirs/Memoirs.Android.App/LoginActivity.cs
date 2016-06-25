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

namespace Memoirs.Android.App
{
    [Activity(Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            // Create your application here
            var loginButton = FindViewById<Button>(Resource.Id.login_page_login_button);
            loginButton.Click += LoginButton_Click;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof (MainActivity));
            StartActivity(intent);
        }
    }
}