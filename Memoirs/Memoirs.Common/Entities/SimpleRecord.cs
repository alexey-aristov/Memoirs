using System;
using Memoirs.Common.Entities.Abstract;

namespace Memoirs.Common.Entities {
	public class SimpleRecord : RecordBase {
        public override string Label { get; set; }
        public override string Text { get; set; }
        public override string Description { get; set; }

        public override int Id { get; set; }
        public override bool IsDeleted { get; set; }
        public override DateTime DateCreated { get; set; }
    }
}
