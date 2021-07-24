using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.ViewModels
{
    public class WorkTaskViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int Priority { get; set; }
        public int DisplayOrder { get; set; } = 1;
        public int Status { get; set; } = 1;
        public string AssignToUserID { get; set; }
    }
}
