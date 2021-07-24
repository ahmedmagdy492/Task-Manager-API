using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Manager.Repository;
using Task_Manager.UnitOfWork;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupController : ControllerBase
    {
        [HttpGet]
        [Route("StatusList")]
        public IActionResult GetStatusList()
        {
            return Ok(new List<object>
            {
                new { Id = 1, Title = "Todo" },
                new { Id = 2, Title = "Doing" },
                new { Id = 3, Title = "Done" }
            });
        }

        [HttpGet]
        [Route("PriorityList")]
        public IActionResult GetPriorityList()
        {
            return Ok(new List<object>
            {
                new { Id = 1, Title = "Low" },
                new { Id = 2, Title = "Normal" },
                new { Id = 3, Title = "High" }
            });
        }
    }
}
