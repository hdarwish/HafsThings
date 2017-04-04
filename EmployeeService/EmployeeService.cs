using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class EmployeeService : IEmployeeService
    {
        public EmployeeInfo GetEmployee(EmplpoyeeRequest empRequest)
        {
            Employee employee = null;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParm = new SqlParameter("@Id", empRequest.EmployeeId);
                cmd.Parameters.Add(idParm);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((EmployeeType)reader["EmployeeType"] == EmployeeType.FullTimeEmployee)
                    {
                        employee = new FullTimeEmployee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = (reader["Name"]).ToString(),
                            Gender = (reader["Gender"]).ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Type = EmployeeType.FullTimeEmployee,
                            AnnualSalary = Convert.ToInt32(reader["AnnualSalary"])
                        };
                    }
                    else
                    {
                        employee = new PartTimeEmployee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = (reader["Name"]).ToString(),
                            Gender = (reader["Gender"]).ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Type = EmployeeType.PartTimeEmployee,
                            HourlyPay = Convert.ToInt32(reader["HourlyPay"]),
                            HoursWorked = Convert.ToInt32(reader["HoursWorked"])
                        };
                    }
                }
            }
            return new EmployeeInfo(employee);
        }
        public void SaveEmployee(EmployeeInfo employeeInfo)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spsaveEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter IdParm = new SqlParameter("@Id", employeeInfo.Id);
                SqlParameter NameParm = new SqlParameter("@Name", employeeInfo.Name);
                SqlParameter GenderParm = new SqlParameter("@Gender", employeeInfo.Gender);
                SqlParameter dateOfBirthParm = new SqlParameter("@DateOfBirth", employeeInfo.DOB);
                SqlParameter TypeParm = new SqlParameter("@EmployeeType", employeeInfo.Type);
                if (employeeInfo.Type == EmployeeType.FullTimeEmployee)
                {
                    SqlParameter annualSalaryParm = new SqlParameter("@AnnualSalary", employeeInfo.AnnualSalary);
                    cmd.Parameters.Add(annualSalaryParm);
                }
                else
                {
                    SqlParameter HourlyPayParm = new SqlParameter("@HourlyPay", employeeInfo.HourlyPay);
                    cmd.Parameters.Add(HourlyPayParm);
                    SqlParameter HoursWorkedParm = new SqlParameter("@HoursWorked", employeeInfo.HoursWorked);
                    cmd.Parameters.Add(HoursWorkedParm);
                }
                cmd.Parameters.Add(IdParm);
                cmd.Parameters.Add(NameParm);
                cmd.Parameters.Add(GenderParm);
                cmd.Parameters.Add(dateOfBirthParm);
                cmd.Parameters.Add(TypeParm);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
