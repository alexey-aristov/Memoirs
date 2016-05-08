using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memoirs.Web.Models
{
    public class RecordModel
    {
        public virtual string Label { get; set; }
        public virtual string Text { get; set; }
        public virtual string Description { get; set; }

        public virtual int Id { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}