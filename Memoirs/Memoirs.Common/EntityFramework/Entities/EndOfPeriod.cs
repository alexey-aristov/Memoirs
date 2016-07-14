using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memoirs.Common.EndOfPeriod;
using Memoirs.Common.EntityFramework.Entities.Abstract;

namespace Memoirs.Common.EntityFramework.Entities
{
    public class EndOfPeriod:IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int? Month { get; set; }
        public int? Week { get; set; }
        public EndOfPeriodEnum EndOfPeriodType { get; set; }
        
        public int RecordId { get; set; }
        public Record Record { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
