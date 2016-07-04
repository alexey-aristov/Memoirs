using System;
using System.Collections.Generic;
using System.Net;
using Memoirs.Android.Common.Login;
using Memoirs.Shared;

namespace Memoirs.Android.Common.Records
{
    public class WebApiRecordsProvider : IRecordsProvider
    {
        private string apiUrl = "http://aristov.me/api/records";
        private IUserManager _userManager;
        public WebApiRecordsProvider(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public List<Record> GetRecords()
        {
            HttpStatusCode st;
            var records = WebApiCaller.Get<List<Record>>(apiUrl, out st, null, _userManager.User.Token);
            return records;
        }

        public Record Get(int id)
        {
            HttpStatusCode st;
            var record = WebApiCaller.Get<Record>(apiUrl + "/" + id, out st, null, _userManager.User.Token);
            return record;
        }

        public List<Record> GetFiltered(DateTime monthyear, RecordsGetType getType)
        {
            HttpStatusCode st;
            var records = WebApiCaller.Get<List<Record>>(apiUrl, out st, new Dictionary<string, string>() {
                { "monthyear", $"{monthyear.Month}.{monthyear.Year}"},
                { "gettype", getType.ToString()},

            }, _userManager.User.Token);
            return records;
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