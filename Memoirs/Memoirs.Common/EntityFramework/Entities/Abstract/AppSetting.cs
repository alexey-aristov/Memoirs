using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memoirs.Common.EntityFramework.Entities.Abstract
{
    public class AppSetting:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(120)]
        [Index(IsUnique = true)]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
