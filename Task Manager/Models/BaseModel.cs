using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Models
{
    public class BaseModel<TID>
    {
        public TID Id { get; set; }
    }
}
