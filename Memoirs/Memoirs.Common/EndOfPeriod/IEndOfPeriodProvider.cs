using System.Collections.Generic;

namespace Memoirs.Common.EndOfPeriod
{
    public interface IEndOfPeriodProvider
    {
        IEnumerable<EndOfPeriodBase> GetAllUnfilledPeriods(string userId);

        IEnumerable<EndOfYearPeriod> GetUnfilledYearPeriods(string userId);
        IEnumerable<EndOfMonthPeriod> GetUnfilledMonthPeriods(string userId);
        IEnumerable<EndOfWeekPeriod> GetUnfilledWeekPeriods(string userId);

        IEnumerable<EndOfYearPeriod> GetFilledYearPeriods(string userId);
        IEnumerable<EndOfMonthPeriod> GetFilledMonthPeriods(string userId);
        IEnumerable<EndOfWeekPeriod> GetFilledWeekPeriods(string userId);


    }
}
