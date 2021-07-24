using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Models
{
    public class GroupUser
    {
        public string GroupID { get; set; }
        public string UserID { get; set; }
        public bool? IsAdmin { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
    }
}
