using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService
{
    [ServiceContract(CallbackContract =(typeof(IEmployeeServiceCallback)))]
    public interface IEmployeeService
    {

        [OperationContract] 
        EmployeeInfo GetEmployee(EmplpoyeeRequest empRequest);

        [OperationContract(IsOneWay = true)]
        void SaveEmployee(EmployeeInfo employeeInfo);

    }
    public interface IEmployeeServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void progress(int percentageCompleted);
    }
}
