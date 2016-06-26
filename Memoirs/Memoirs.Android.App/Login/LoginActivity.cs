using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Ninject;

namespace Memoirs.Android.App.Login
{
    [Activity(MainLauncher = true)]
    public class LoginActivity : Activity
    {
        private ILoginProvider _loginProvider;
        private Button _loginButton;
        private EditText _loginEditText;
        private EditText _passwordEditText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);
            
            _loginProvider = App.Container.Get<ILoginProvider>();

            _loginButton = FindViewById<Button>(Resource.Id.login_page_login_button);
            _loginEditText = FindViewById<EditText>(Resource.Id.login_page_login_edittext);
            _passwordEditText = FindViewById<EditText>(Resource.Id.login_page_password_edittext);
            _loginButton.Click += LoginButton_Click;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (_loginProvider.Login(_loginEditText.Text, _passwordEditText.Text) != null)
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
        }
    }
}