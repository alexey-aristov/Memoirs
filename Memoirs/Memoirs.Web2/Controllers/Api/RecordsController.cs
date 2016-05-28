﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Memoirs.Common;
using Memoirs.Common.Entities;
using Memoirs.Common.Entities.Abstract;
using Memoirs.Identity;
using Memoirs.Web2.Models;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;

namespace Memoirs.Web2.Controllers.Api
{
    [Authorize]
    public class RecordsController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;
        public RecordsController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: api/Records
        public IEnumerable<RecordModel> Get()
        {
            var user = User.Identity.Name;
            var records = _unitOfWork.RecordsRepository.Get().Select(a =>
                new RecordModel()
                {
                    Text = a.Text,
                    DateCreated = a.DateCreated,
                    Description = a.Description,
                    Id = a.Id,
                    IsDeleted = a.IsDeleted,
                    Label = a.Label
                }).ToList();
            records.ForEach(a => a.Editable = IsEditable(a.DateCreated));
            return records;
        }

        private bool IsEditable(DateTime recordDateCreated)
        {
            return recordDateCreated == default(DateTime) || DateTime.Now - recordDateCreated < TimeSpan.FromDays(2);
        }

        [HttpGet]
        public RecordModel Get(int id)
        {
            var model = new RecordModel(_unitOfWork.RecordsRepository.GetById(id));
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
                }).ToList();
            records.ForEach(a => a.Editable = IsEditable(a.DateCreated));
            return records;
        }

        // POST: api/Records
        public int Post([FromBody]RecordModel value)
        {
            if (value.Id != 0)
            {
                throw new ArgumentException("Atempt to create Record with Id != 0. For update use put method");
            }
            var record = new SimpleRecord()
            {
                Text = value.Text,
                DateCreated = DateTime.Now,
                Description = value.Description,
                Id = value.Id,
                IsDeleted = value.IsDeleted,
                Label = value.Label
            };
            _unitOfWork.RecordsRepository.Add(record);
            _unitOfWork.Save();
            return record.Id;
        }

        [HttpPut]
        public void Put([FromBody]RecordModel value)
        {
            if (value.Id == 0)
                throw new ArgumentException("put for new record");
            var record = _unitOfWork.RecordsRepository.GetById(value.Id);

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