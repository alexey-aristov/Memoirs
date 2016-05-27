using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memoirs.Common.Entities.Abstract;

namespace Memoirs.Web2.Models
{
    public class RecordModel
    {
        public RecordModel()
        {
        }

        public RecordModel(RecordBase recordBase)
        {
            Text = recordBase.Text;
            DateCreated = recordBase.DateCreated;
            Description = recordBase.Description;
            Id = recordBase.Id;
            IsDeleted = recordBase.IsDeleted;
            Label = recordBase.Label;
        }

        public  string Label { get; set; }
        public  string Text { get; set; }
        public  string Description { get; set; }
        public  int Id { get; set; }
        public  bool IsDeleted { get; set; }
        public  DateTime DateCreated { get; set; }
    }
}