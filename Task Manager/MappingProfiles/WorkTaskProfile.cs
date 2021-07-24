using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Manager.Models;
using Task_Manager.ViewModels;

namespace Task_Manager.MappingProfiles
{
    public class WorkTaskProfile : Profile
    {
        public WorkTaskProfile()
        {
            CreateMap<WorkTaskViewModel, WorkTask>();
        }
    }
}
