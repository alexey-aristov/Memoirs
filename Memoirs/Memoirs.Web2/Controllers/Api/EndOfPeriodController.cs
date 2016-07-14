using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Memoirs.Common.EndOfPeriod;
using Memoirs.Common.EntityFramework.Entities;
using Memoirs.Web2.Models.Api;
using Microsoft.AspNet.Identity;

namespace Memoirs.Web2.Controllers.Api
{
    public class EndOfPeriodController : ApiController
    {
        private IEndOfPeriodProvider _endOfPeriodProvider;
        public EndOfPeriodController(IEndOfPeriodProvider endOfPeriodProvider)
        {
            _endOfPeriodProvider = endOfPeriodProvider;
        }

        public List<EndOfPeriodBase> Get(EndOfPeriodEnum periodType, EndOfPeriodFilledType filledType)
        {
            switch (filledType)
            {
                case EndOfPeriodFilledType.Filled:
                    switch (periodType)
                    {
                        case EndOfPeriodEnum.Year:
                            return _endOfPeriodProvider.GetFilledYearPeriods(User.Identity.GetUserId()).Cast<EndOfPeriodBase>().ToList();

                        case EndOfPeriodEnum.Month:
                            return _endOfPeriodProvider.GetFilledMonthPeriods(User.Identity.GetUserId()).Cast<EndOfPeriodBase>().ToList();

                        case EndOfPeriodEnum.Week:
                            return _endOfPeriodProvider.GetFilledWeekPeriods(User.Identity.GetUserId()).Cast<EndOfPeriodBase>().ToList();
                        default:
                            throw new Exception("Unknown PeriodType");
                    }
                case EndOfPeriodFilledType.Unfilled:
                    switch (periodType)
                    {
                        case EndOfPeriodEnum.Year:
                            return _endOfPeriodProvider.GetUnfilledYearPeriods(User.Identity.GetUserId()).Cast<EndOfPeriodBase>().ToList();

                        case EndOfPeriodEnum.Month:
                            return _endOfPeriodProvider.GetUnfilledMonthPeriods(User.Identity.GetUserId()).Cast<EndOfPeriodBase>().ToList();

                        case EndOfPeriodEnum.Week:
                            return _endOfPeriodProvider.GetUnfilledWeekPeriods(User.Identity.GetUserId()).Cast<EndOfPeriodBase>().ToList();
                        default:
                            throw new Exception("Unknown PeriodType");
                    }
                default: throw new Exception();
            }
        }
    }
}
