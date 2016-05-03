﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Memoirs.Common.Entities.Enums;

namespace Memoirs.Common.Entities.Abstract {
	[Table("Records")]
	public abstract class RecordBase:IEntity {
		public virtual string Label { get; set; }
		public virtual string Text { get; set; }
		public virtual string Description { get; set; }

		public virtual int Id { get; set; }
		public virtual bool IsDeleted { get; set; }
		public virtual DateTime DateCreated { get; set; }
	}
}