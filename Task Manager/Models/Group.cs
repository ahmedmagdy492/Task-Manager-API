using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Models
{
    public class Group : BaseModel<string>
    {
        public Group()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public string Name { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<WorkTask> WorkTasks { get; set; }
        public ICollection<GroupMessage> GroupMessages { get; set; }
    }
}
