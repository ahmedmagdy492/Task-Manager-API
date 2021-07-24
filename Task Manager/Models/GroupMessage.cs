using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Models
{
    public class GroupMessage : BaseModel<string>
    {
        public GroupMessage()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
        public string SenderId { get; set; }
        public string GroupId { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
    }
}
