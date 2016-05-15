using System;

namespace Memoirs.Common
{
    public class MemoirsExceptionBase : Exception
    {
        /// <summary>
        /// moot
        /// </summary>
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
