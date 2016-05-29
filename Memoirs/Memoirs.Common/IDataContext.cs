using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memoirs.Common.Entities.Abstract;

namespace Memoirs.Common
{
    public interface IDataContext
    {
        IQueryable<RecordBase> RecordsQuery { get; set; }
    }
}
