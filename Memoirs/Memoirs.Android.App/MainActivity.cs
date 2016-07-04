using System;
using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Memoirs.Android.Common;
using Memoirs.Android.Common.Login;
using Memoirs.Android.Common.Records;
using Memoirs.Shared;
using Ninject;

namespace Memoirs.Android.App
{
    [Activity]
    public class MainActivity : Activity
    {
        private ListView _recordsListView;

        IRecordsProvider _recordsProvider;
        //private User _user;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //if (App.CurrentUser==null)
            //{
            //    var intent = new Intent(this, typeof(LoginActivity));
            //    StartActivity(intent);
            //    return;
            //}
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var _loginProvider = Dependencies.Container.Get<ILoginProvider>();
            _recordsProvider = Dependencies.Container.Get<IRecordsProvider>();
            _recordsListView = FindViewById<ListView>(Resource.Id.main_records_list);
            
            
            var records = _recordsProvider.GetFiltered(DateTime.Now, RecordsGetType.Exact);
            _recordsListView.Adapter=new ArrayAdapter(this, Resource.Layout.RecordsListView, records);


            Button button = FindViewById<Button>(Resource.Id.MyButton);
            
        }
    }
}

