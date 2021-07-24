using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Manager.Helpers;
using Task_Manager.Models;
using Task_Manager.UnitOfWork;
using Task_Manager.ViewModels;

namespace Task_Manager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkTaskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkTaskController(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("TodaysTasks")]
        public IActionResult GetAll(int? pageNo, int? pageSize)
        {
            try
            {
                if(pageNo == null || pageSize == null)
                {
                    return BadRequest(new { success = false, data = new List<string> { "Invalid Parameters" } });
                }

                var todaysDate = DateTime.Now.ToString("yyyyMMdd");
                var todaysWorkTasks = _unitOfWork.WorkTaskRepository.GetAll(wt => wt.CreationDate == todaysDate && wt.CreatorId == User.Identity.Name).Skip((pageNo.Value - 1) * pageSize.Value).Take(pageSize.Value);

                return Ok(new { success = true, data = todaysWorkTasks });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }

        [HttpPost]
        public IActionResult Add(WorkTaskViewModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, data = ValidationsHelper.GetErrorMsgs(ModelState.Values) });
                }

                WorkTask workTask = _mapper.Map<WorkTask>(model);
                workTask.CreatorId = User.Identity.Name;
                workTask.CreationDate = DateTime.Now.ToString("yyyyMMdd");
                _unitOfWork.WorkTaskRepository.Add(workTask);
                var saveResult = _unitOfWork.Save();

                if(!saveResult)
                {
                    return BadRequest(new { success = false, data = new List<string> { "Error Occured While Saving" } });
                }

                return Created("", new { success = true, data = workTask });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest(new { success = false, data = new List<string> { "Invalid ID" } });
                }

                var workTask = _unitOfWork.WorkTaskRepository.GetOne(w => w.Id == id);

                if (workTask == null)
                    return NotFound(new { success = false, data = new List<string> { "Work Task Not Found" } });

                _unitOfWork.WorkTaskRepository.Remove(workTask);
                var saveResult = _unitOfWork.Save();

                if(!saveResult)
                {
                    return BadRequest(new { success = false, data = new List<string> { "Error Occured While Saving" } });
                }

                return Ok(new { success = true, data = new List<string> { "Deleted Successfully" } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }

        [HttpPatch]
        public IActionResult Update(WorkTaskViewModel model, string id)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(id ))
                    return BadRequest(new { success = false, data = new List<string> { "Invalid ID" } });

                if(!ModelState.IsValid)
                    return BadRequest(new { success = false, data = ValidationsHelper.GetErrorMsgs(ModelState.Values) });

                var workTask = _unitOfWork.WorkTaskRepository.GetOne(wt => wt.Id == id);
                if (workTask == null)
                    return NotFound(new { success = false, data = new List<string> { "Work Task Not Found" } });

                _unitOfWork.WorkTaskRepository.Edit(workTask);
                var saveResult = _unitOfWork.Save();

                if (!saveResult)
                    return BadRequest(new { success = false, data = new List<string> { "Error Occured While Saving" } });

                return Ok(new { success = true, data = "Saved Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }
    }
}
