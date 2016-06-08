using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Memoirs.Common;
using Memoirs.Common.EntityFramework;
using Memoirs.Common.EntityFramework.Entities;
using Memoirs.Common.Identity;
using Memoirs.Web2.Models;
using Microsoft.AspNet.Identity;

namespace Memoirs.Web2.Controllers.Api
{
    [Authorize]
    public class RecordsController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;
        private ILogger _logger;
        private string _dateTimeFormat;

        public RecordsController(IUnitOfWork unitOfWork, ApplicationUserManager userManager, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _logger = logger;
            _dateTimeFormat = "yyyy-MM-dd";
        }

        // GET: api/Records
        public IEnumerable<RecordModel> Get()
        {
            _logger.Debug("get all");
            var user = User.Identity.Name;
            var records = _userManager.FindById(User.Identity.GetUserId()).Records.Select(a =>
                new RecordModel()
                {
                    Text = a.Text,
                    DateCreated = a.DateCreated,
                    Description = a.Description,
                    Id = a.Id,
                    IsDeleted = a.IsDeleted,
                    Label = a.Label
                }).OrderBy(a => a.DateCreated).ToList();
            records.ForEach(a =>
            {
                a.DateCreatedString = a.DateCreated.ToString(_dateTimeFormat);
                a.Editable = IsEditable(a.DateCreated);
            });
            return records;
        }

        private bool IsEditable(DateTime recordDateCreated)
        {
            return recordDateCreated == default(DateTime) || DateTime.Now - recordDateCreated < TimeSpan.FromDays(2);
        }

        [HttpGet]
        public RecordModel Get(int id)
        {
            _logger.Debug("get {0}",id.ToString());
            var model =
                new RecordModel(_userManager.FindById(User.Identity.GetUserId()).Records.FirstOrDefault(a => a.Id == id));

            model.DateCreatedString = model.DateCreated.ToString(_dateTimeFormat);
            model.Editable = IsEditable(model.DateCreated);
            return model;
        }
        [HttpGet]
        public IEnumerable<RecordModel> Get(string monthyear)
        {
            DateTime dateTime = DateTime.Parse(monthyear);
            var records = _unitOfWork.RecordsRepository.Get()
                .Where(a => a.DateCreated.Month == dateTime.Month && dateTime.Year == a.DateCreated.Year)
                .Select(a => new RecordModel()
                {
                    Text = a.Text,
                    DateCreated = a.DateCreated,
                    Description = a.Description,
                    Id = a.Id,
                    IsDeleted = a.IsDeleted,
                    Label = a.Label
                }).OrderBy(a => a.DateCreated).ToList();
            records.ForEach(a =>
            {
                a.DateCreatedString = a.DateCreated.ToString(_dateTimeFormat);
                a.Editable = IsEditable(a.DateCreated);
            });
            return records;
        }

        // POST: api/Records
        public int Post([FromBody]RecordModel value)
        {
            if (value.Id != 0)
            {
                throw new ArgumentException("Atempt to create Record with Id != 0. For update use put method");
            }
            var todayRecord = _userManager.FindById(User.Identity.GetUserId()).Records.FirstOrDefault(a => a.DateCreated.Month == DateTime.Now.Month && a.DateCreated.Year == DateTime.Now.Year && a.DateCreated.Day == DateTime.Now.Day);
            if (todayRecord != null)
            {
                throw new ArgumentException("Cannot create second record for a day");
            }

            var record = new Record()
            {
                Text = value.Text,
                DateCreated = DateTime.Now,
                Description = value.Description,
                Id = value.Id,
                IsDeleted = value.IsDeleted,
                Label = value.Label,
                UserId = User.Identity.GetUserId()
            };
            _unitOfWork.RecordsRepository.Add(record);
            _unitOfWork.Save();
            return record.Id;
        }

        [HttpPut]
        public void Put([FromBody]RecordModel value)
        {
            if (value.Id == 0)
                throw new ArgumentException("put for new recordis not allowed");
            var record = _userManager.FindById(User.Identity.GetUserId()).Records.FirstOrDefault(a => a.Id == value.Id);
            if (record == null)
                throw new ArgumentException("record does not exist");

            if (!IsEditable(record.DateCreated))
            {
                return;
            }
            record.Text = value.Text;
            record.Description = value.Description;
            record.IsDeleted = value.IsDeleted;
            record.Label = value.Label;

            _unitOfWork.RecordsRepository.Update(record);
            _unitOfWork.Save();

        }

        // DELETE: api/Records/5
        public void Delete(int id)
        {
        }
    }
}
