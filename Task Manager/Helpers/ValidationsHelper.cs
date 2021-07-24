using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace Task_Manager.Helpers
{
    public static class ValidationsHelper
    {
        public static List<string> GetErrorMsgs(ValueEnumerable stateEntries)
        {
            List<string> msgs = new();

            foreach(var err in stateEntries)
            {
                if(err.Errors.Count > 0)
                {
                    msgs.Add(err.Errors[0].ErrorMessage);
                }
            }

            return msgs;
        }
    }
}
