using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.ViewModels
{
    public class GroupViewModel
    {
        [Required]
        public string GroupName { get; set; }
    }
}
