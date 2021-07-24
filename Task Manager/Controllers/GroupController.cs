using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Manager.Models;
using Task_Manager.UnitOfWork;
using Task_Manager.ViewModels;

namespace Task_Manager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetMyGroups(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return BadRequest(new { success = false, data = new List<string> { "Invalid User Id" } });

                var user = _unitOfWork.UserRepository.GetOne(u => u.Id == userId, "GroupUsers");
                if(user == null)
                    return NotFound(new { success = false, data = new List<string> { "User not Found" } });

                return Ok(new { success = true, data = user.GroupUsers.Where(u => u.UserID == userId).ToList() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }

        [HttpGet("Details")]
        public IActionResult GetGroupDetails(string groupId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(groupId))
                    return BadRequest(new { success = false, data = new List<string> { "Invalid Group ID" } });

                var group = _unitOfWork.GroupRepository.GetOne(g => g.Id == groupId);
                return Ok(new { success = true, data = group });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }

        [HttpPost]
        public IActionResult AddGroup(string groupName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(groupName))
                    return BadRequest(new { success = false, data = new List<string> { "Please Speicify Group Name" } });

                var group = new Group
                {
                    Name = groupName
                };
                _unitOfWork.GroupRepository.Add(group);

                var saveResult = _unitOfWork.Save();
                if (!saveResult)
                    return BadRequest(new { success = false, data = new List<string> { "Error Occured While Saving" } });

                var userId = User.Identity.Name;
                var creator = _unitOfWork.UserRepository.GetOne(u => u.Id == userId, "GroupUsers");
                creator.GroupUsers.Add(new GroupUser
                {
                    UserID = userId,
                    GroupID = group.Id,
                    IsAdmin = true
                });

                var addUserToGroupResult = _unitOfWork.Save();
                if (!addUserToGroupResult)
                    return BadRequest(new { success = false, data = new List<string> { "Error Occured While Saving" } });

                return Created("", new { success = true, data = group });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }


        #region Group Work Tasks
        [HttpGet("WorkTasks")]
        public IActionResult GetGroupWorkTasks(string groupId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(groupId))
                    return BadRequest(new { success = false, data = new List<string> { "Invalid Group ID" } });

                var group = _unitOfWork.GroupRepository.GetOne(g => g.Id == groupId, "WorkTasks");
                if (group == null)
                    return NotFound(new { success = false, data = new List<string> { "Group Not Found" } });

                return Ok(new { success = true, data = group.WorkTasks.ToList() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }

        [HttpPost("WorkTasks")]
        public IActionResult AddWorkTaskToGroup(string groupId, string workTaskId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(groupId) || string.IsNullOrWhiteSpace(workTaskId))
                    return BadRequest(new { success = false, data = new List<string> { "Invalid Params Provided" } });

                var group = _unitOfWork.GroupRepository.GetOne(g => g.Id == groupId, "WorkTasks");
                if(group == null)
                    return NotFound(new { success = false, data = new List<string> { "Group Not Found" } });

                var workTask = _unitOfWork.WorkTaskRepository.GetOne(w => w.Id == workTaskId);
                if(workTask == null)
                    return NotFound(new { success = false, data = new List<string> { "Work Task Not Found" } });

                group.WorkTasks.Add(workTask);
                var saveResult = _unitOfWork.Save();
                if(!saveResult)
                    return BadRequest(new { success = false, data = new List<string> { "Error Occured While Saving" } });

                return Ok(new { success = true, data = new List<string> { "Work Task Added Successfully" } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }
        #endregion


        #region Group Users Actions
        [HttpGet("Users")]
        public IActionResult GetUsersOfGroup(string groupId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(groupId))
                    return BadRequest(new { success = false, data = new List<string> { "Invalid GroupId" } });

                var group = _unitOfWork.GroupRepository.GetOne(g => g.Id == groupId, "GroupUsers");
                if (group == null)
                    return NotFound(new { success = false, data = new List<string> { "Group Not Found" } });

                return Ok(new { success = true, data = group.GroupUsers.ToList() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }

        [HttpPost("Users")]
        public IActionResult AddUserToGroup(string userId, string groupId)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(groupId))
                    return BadRequest(new { success = false, data = new List<string> { "Invalid Params Provided" } });

                var group = _unitOfWork.GroupRepository.GetOne(u => u.Id == groupId, "GroupUsers");

                if (group == null)
                    return NotFound(new { success = false, data = new List<string> { "Group Not Found" } });

                group.GroupUsers.Add(new GroupUser
                {
                    UserID = userId,
                    GroupID = groupId,
                    IsAdmin = false
                });

                var saveResult = _unitOfWork.Save();
                if (!saveResult)
                    return BadRequest(new { success = false, data = new List<string> { "Error Occured While Saving" } });

                return Ok(new { success = true, data = new List<string> { "User Added Successfully" } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }
        #endregion
    }
}
