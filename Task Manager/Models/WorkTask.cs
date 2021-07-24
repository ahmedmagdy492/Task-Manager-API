using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Models
{
    public class WorkTask : BaseModel<string>
    {
        public WorkTask()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public string CreationDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? DisplayOrder { get; set; } = 1;
        public int Status { get; set; } = 1;
        public string CreatorId { get; set; }
        public string AssignedToId { get; set; }
        public int Priority { get; set; } = 1;
        public string GroupId { get; set; }

        public User AssignedTo { get; set; }
        public User Creator { get; set; }
        public Group Group { get; set; }
    }
}
