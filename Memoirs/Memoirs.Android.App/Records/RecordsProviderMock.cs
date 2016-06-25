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

namespace Memoirs.Android.App.Records
{
    public class RecordsProviderMock : IRecordsProvider
    {
        public List<Record> GetRecords()
        {
            return new List<Record>()
            {
                new Record()
                {
                    Id = 1,
                    DateCreated = DateTime.Now,
                    Label = "label 1",
                    Text = "text 1",
                    Description = "desc 1",
                    IsDeleted = false
                },
                new Record()
                {
                    Id = 2,
                    DateCreated = DateTime.Now.AddDays(-1),
                    Label = "label 2",
                    Text = "text 2",
                    Description = "desc 2",
                    IsDeleted = false
                },
                new Record()
                {
                    Id = 3,
                    DateCreated = DateTime.Now.AddDays(-2),
                    Label = "label 3",
                    Text = "text 3",
                    Description = "desc 3",
                    IsDeleted = false
                }

            };
        }
    }
}