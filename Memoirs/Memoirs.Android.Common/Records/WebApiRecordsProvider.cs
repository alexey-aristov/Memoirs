using System;
using System.Collections.Generic;
using Memoirs.Shared;

namespace Memoirs.Android.Common.Records
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