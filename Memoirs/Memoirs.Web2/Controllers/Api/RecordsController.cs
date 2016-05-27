using System;
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
            return _unitOfWork.RecordsRepository.Get().Select(a =>
                new RecordModel()
                {
                    Text = a.Text,
                    DateCreated = a.DateCreated,
                    Description = a.Description,
                    Id = a.Id,
                    IsDeleted = a.IsDeleted,
                    Label = a.Label
                }).ToList();
        }

        // GET: api/Records/5
        public RecordModel Get(int id)
        {
            return new RecordModel(_unitOfWork.RecordsRepository.GetById(id));
        }

        // POST: api/Records
        public void Post([FromBody]RecordModel value)
        {
            if (value.Id != 0)
            {
                throw new ArgumentException("Atempt to create Record with Id");
            }
            _unitOfWork.RecordsRepository.Add(new SimpleRecord()
            {
                Text = value.Text,
                DateCreated = value.DateCreated,
                Description = value.Description,
                Id = value.Id,
                IsDeleted = value.IsDeleted,
                Label = value.Label
            });
            _unitOfWork.Save();
        }

        // PUT: api/Records/5
        public void Put(int id, [FromBody]RecordModel value)
        {
            var record = _unitOfWork.RecordsRepository.GetById(id);
            record.Text = value.Text;
            record.DateCreated = value.DateCreated;
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
