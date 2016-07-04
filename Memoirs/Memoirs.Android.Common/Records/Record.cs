using System;

namespace Memoirs.Android.Common.Records
{
    public class Record
    {
        public string Label { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public override string ToString()
        {
            return $@"{Label}:{Text}:{DateCreated.ToString("yy-MM-dd")}";
        }
    }
}