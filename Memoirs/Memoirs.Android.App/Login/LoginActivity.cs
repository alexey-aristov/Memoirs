using System;
using System.Linq;
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
        private Button _registerButton;
        private Button _registerLaterButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            _loginProvider = App.Container.Get<ILoginProvider>();

            _loginButton = FindViewById<Button>(Resource.Id.login_page_login_button);
            _loginEditText = FindViewById<EditText>(Resource.Id.login_page_login_edittext);
            _passwordEditText = FindViewById<EditText>(Resource.Id.login_page_password_edittext);
            _registerButton = FindViewById<Button>(Resource.Id.login_page_register_button);
            _registerLaterButton = FindViewById<Button>(Resource.Id.login_page_register_later_button);
            _loginButton.Click += _loginButton_Click;
            _registerLaterButton.Click += _registerLaterButton_Click;
        }

        private void _registerLaterButton_Click(object sender, EventArgs e)
        {
            var alert = new AlertDialog.Builder(this).SetMessage("test").SetPositiveButton("yes?", (o, args) => { });
            alert.Create().Show();
        }

        private void _loginButton_Click(object sender, EventArgs e)
        {
            var loginResutl = _loginProvider.LoginAsync(_loginEditText.Text, _passwordEditText.Text).Result;
            if (loginResutl.Errors == null)
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
            else
            {
                var alert = new AlertDialog.Builder(this)
                    .SetMessage(loginResutl.Errors.Aggregate("", (s, s1) => $"{s}{s1}\n"))
                    .SetPositiveButton("ok", (o, args) => { });
                alert.Create().Show();
            }
        }
    }
}