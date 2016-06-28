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
using Memoirs.Shared;

namespace Memoirs.Android.App.Records
{
    public class WebApiRecordsProvider:IRecordsProvider
    {
        public List<Record> GetRecords()
        {
            throw new NotImplementedException();
        }

        public Record Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Record> GetFiltered(DateTime monthyear, RecordsGetType getType)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Record record)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}