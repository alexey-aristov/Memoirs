using System;
using System.Collections.Generic;
using Memoirs.Shared;

namespace Memoirs.Android.Common.Records
{
    public interface IRecordsProvider
    {
        List<Record> GetRecords();
        Record Get(int id);
        List<Record> GetFiltered(DateTime monthyear, RecordsGetType getType);
        void Update(int id, Record record);
        void Remove(int id);
    }
}