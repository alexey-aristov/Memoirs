using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Memoirs.Android.App.Account;
using Memoirs.Android.App.Login;
using Memoirs.Android.App.Records;
using Ninject;

namespace Memoirs.Android.App
{
    [Activity(Label = "Memoirs.Android.App", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ListView _recordsListView;
        private User _user;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var _loginProvider = App.Container.Get<ILoginProvider>();
            var _recordsProvider = App.Container.Get<IRecordsProvider>();
            _user = _loginProvider.GetCurrentUser();
            _recordsListView = FindViewById<ListView>(Resource.Id.main_records_list);
            _recordsListView.Adapter=new ArrayAdapter(this, Resource.Layout.RecordsListView, _recordsProvider.GetRecords());
            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            
        }
    }
}

