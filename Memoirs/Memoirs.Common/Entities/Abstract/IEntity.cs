﻿namespace Memoirs.Common.Entities.Abstract {
	public interface IEntity {
		int Id { get; set; }
		bool IsDeleted { get; set; }
	}
}
