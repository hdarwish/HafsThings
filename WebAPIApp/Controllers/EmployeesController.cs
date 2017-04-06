using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbDataAccess;

namespace WebAPIApp.Controllers
{
    public class EmployeesController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage LoadAllEmployees(string gender="All")
        {
            using (sampleDBEntities entity = new sampleDBEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entity.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entity.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entity.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            "Value for gender must be all, male or female "+ gender + " is invalid" );


                }
               
            }

        }

        // GET api/<controller>/5
        public HttpResponseMessage GetEmployeeById(int id)
        {
            using (sampleDBEntities entity = new sampleDBEntities())
            {
                var ent = entity.Employees.FirstOrDefault(e => e.Id == id);
                if (ent != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ent);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Employee with ID: " + id + " is not found");
                }
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                using (sampleDBEntities entity = new sampleDBEntities())
                {

                    entity.Employees.Add(employee);
                    entity.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }

        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            try
            {
                using (sampleDBEntities entity = new sampleDBEntities())
                {
                    var ent = entity.Employees.FirstOrDefault(e => e.Id == id);
                    if (ent == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                       "Employee with ID: " + id + " is not found");
                    }
                    else
                    {
                        ent.Name = employee.Name;
                        ent.Gender = employee.Gender;
                        ent.DateOfBirth = employee.DateOfBirth;
                        ent.AnnualSalary = employee.AnnualSalary;
                        ent.EmployeeType = employee.EmployeeType;
                        ent.HourlyPay = employee.HourlyPay;
                        ent.HoursWorked = employee.HoursWorked;
                        entity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, ent);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (sampleDBEntities entity = new sampleDBEntities())
                {
                    var ent = entity.Employees.FirstOrDefault(e => e.Id == id);
                    if (ent == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                       "Employee with ID: " + id + " is not found");
                    }
                    else
                    {
                        entity.Employees.Remove(ent);
                        entity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }
    }
}