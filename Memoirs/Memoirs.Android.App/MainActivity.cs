using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Memoirs.Android.App.Account;
using Memoirs.Android.App.Login;
using Memoirs.Android.App.Records;
using Ninject;

namespace Memoirs.Android.App
{
    [Activity]
    public class MainActivity : Activity
    {
        private ListView _recordsListView;
        private User _user;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            if (App.CurrentUser==null)
            {
                var intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
                return;
            }
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var _loginProvider = App.Container.Get<ILoginProvider>();
            var _recordsProvider = App.Container.Get<IRecordsProvider>();
            _user = _loginProvider.GetCurrentUser();
            _recordsListView = FindViewById<ListView>(Resource.Id.main_records_list);
            
            HttpStatusCode st;
            var currentRecords = WebApiCaller.Get<List<Record>>("http://aristov.me/api/records", out st, null, App.CurrentUser.Token);

            _recordsListView.Adapter=new ArrayAdapter(this, Resource.Layout.RecordsListView, currentRecords);
            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            
        }
    }
}

