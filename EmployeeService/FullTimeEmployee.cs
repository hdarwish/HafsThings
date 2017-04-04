using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService
{
    [DataContract]
    class FullTimeEmployee : Employee
    {

        [DataMember(Order = 6)]
        public int AnnualSalary { get; set; }
    }
}
