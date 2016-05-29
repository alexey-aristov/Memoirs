using System;
using System.ComponentModel.DataAnnotations.Schema;
using Memoirs.Common.Entities.Abstract;
using Memoirs.Identity;

namespace Memoirs.EntityFramework.Entities {
	public class SimpleRecord : RecordBase {
        public override string Label { get; set; }
        public override string Text { get; set; }
        public override string Description { get; set; }

        public override int Id { get; set; }
        public override bool IsDeleted { get; set; }
        public override DateTime DateCreated { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
