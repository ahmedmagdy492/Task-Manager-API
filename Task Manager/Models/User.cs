using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Models
{
    public class User : BaseModel<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImgUrl { get; set; }

        public ICollection<WorkTask> AssignedToWorkTasks { get; set; }
        public ICollection<WorkTask> MyWorkTasks { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<GroupMessage> Messages { get; set; }
    }
}
