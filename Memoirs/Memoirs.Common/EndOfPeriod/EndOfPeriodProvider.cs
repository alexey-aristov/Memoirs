using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace Memoirs.Common.EndOfPeriod
{
    public class EndOfPeriodProvider : IEndOfPeriodProvider
    {
        private IUnitOfWork _unitOfWork;
        public EndOfPeriodProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<EndOfPeriodBase> GetAllUnfilledPeriods(string userId)
        {
            return null;
        }

        public IEnumerable<EndOfYearPeriod> GetUnfilledYearPeriods(string userId)
        {
            var filled = GetFilledYearPeriods(userId).Select(a=>a.Year).ToList();
            var yearGroups = _unitOfWork.RecordsRepository.Get()
                .GroupBy(a => a.DateCreated.Year).Select(a => a.Key).ToList();

            var relativeComplement = yearGroups.Except(filled);
            return relativeComplement.Select(a => new EndOfYearPeriod()
            {
                Year = a
            }).ToList();
        }

        public IEnumerable<EndOfMonthPeriod> GetUnfilledMonthPeriods(string userId)
        {
            var filled = GetFilledMonthPeriods(userId).Select(a => new
            {
                Year = a.Year,
                Month = a.Month
            }).ToList();
            var yearGroups = _unitOfWork.RecordsRepository.Get()
                .GroupBy(a => new
                {
                    Year = a.DateCreated.Year,
                    Month = a.DateCreated.Month
                }).Select(a => a.Key).ToList();

            var relativeComplement = yearGroups.Except(filled);
            return relativeComplement.Select(a => new EndOfMonthPeriod()
            {
                Year = a.Year,
                Month = a.Month
            }).ToList();
        }

        public IEnumerable<EndOfWeekPeriod> GetUnfilledWeekPeriods(string userId)
        {
            var filled = GetFilledWeekPeriods(userId).Select(a => new
            {
                Year = a.Year,
                Week = a.Week
            }).ToList();
            var yearGroups = _unitOfWork.RecordsRepository.Get()
                .GroupBy(a => new
                {
                    Year = a.DateCreated.Year,
                    Week = SqlFunctions.DatePart("week", a.DateCreated).Value
                }).Select(a => a.Key).ToList();

            var relativeComplement = yearGroups.Except(filled);
            return relativeComplement.Select(a => new EndOfWeekPeriod()
            {
                Year = a.Year,
                Week = a.Week
            }).ToList();
        }

        public IEnumerable<EndOfYearPeriod> GetFilledYearPeriods(string userId)
        {
            return _unitOfWork.EndOfPeriodRepository.Get()
                .Where(a=>a.EndOfPeriodType==EndOfPeriodEnum.Year)
                .ToList()
                .Select(a=>(EndOfYearPeriod)ConvertFromDbEntity(a)).ToList();
        }

        public IEnumerable<EndOfMonthPeriod> GetFilledMonthPeriods(string userId)
        {
            return _unitOfWork.EndOfPeriodRepository.Get()
                .Where(a => a.EndOfPeriodType == EndOfPeriodEnum.Month)
                .ToList()
                .Select(a => (EndOfMonthPeriod)ConvertFromDbEntity(a)).ToList();
        }

        public IEnumerable<EndOfWeekPeriod> GetFilledWeekPeriods(string userId)
        {
            return _unitOfWork.EndOfPeriodRepository.Get()
                .Where(a => a.EndOfPeriodType == EndOfPeriodEnum.Week)
                .ToList()
                .Select(a => (EndOfWeekPeriod)ConvertFromDbEntity(a)).ToList();
        }

        private EndOfPeriodBase ConvertFromDbEntity(EntityFramework.Entities.EndOfPeriod endOfPeriod)
        {
            switch (endOfPeriod.EndOfPeriodType)
            {
                case EndOfPeriodEnum.Year:
                    return new EndOfYearPeriod()
                    {
                        Id = endOfPeriod.Id,
                        Year = endOfPeriod.Year,
                        Description = endOfPeriod.Description,
                        RecordId = endOfPeriod.RecordId
                    };
                case EndOfPeriodEnum.Month:
                    return new EndOfMonthPeriod()
                    {
                        Id = endOfPeriod.Id,
                        Month = endOfPeriod.Month.Value,
                        Year = endOfPeriod.Year,
                        Description = endOfPeriod.Description,
                        RecordId = endOfPeriod.RecordId
                    };
                case EndOfPeriodEnum.Week:
                    return new EndOfWeekPeriod()
                    {
                        Id = endOfPeriod.Id,
                        Year = endOfPeriod.Year,
                        Week = endOfPeriod.Week.Value,
                        Description = endOfPeriod.Description,
                        RecordId = endOfPeriod.RecordId
                    };
                default:
                    throw new ArgumentException("ConvertFromDbEntity, endOfPeriod:" + endOfPeriod);
            }
        }
    }
}
