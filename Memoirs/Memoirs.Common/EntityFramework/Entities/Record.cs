﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Memoirs.Common.EntityFramework.Entities.Abstract;

namespace Memoirs.Common.EntityFramework.Entities {
	public class Record : RecordBase {
        public override string Label { get; set; }
        public override string Text { get; set; }
        public override string Description { get; set; }

        public override int Id { get; set; }
        public override bool IsDeleted { get; set; }
        public override DateTime DateCreated { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int? EndOfPeriodId { get; set; }
        public EndOfPeriod EndOfPeriod { get; set; }
    }
}
