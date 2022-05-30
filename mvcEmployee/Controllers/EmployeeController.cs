using mvcEmployee.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        employeeEntities dbObj = new employeeEntities();
        public ActionResult Employee(emplDetail obj)
        {
            return View(obj);   
        }

        [HttpPost]
        public ActionResult AddEmployee(emplDetail model)
        {
            if (ModelState.IsValid)
            {
                emplDetail obj = new emplDetail();
                obj.firstName = model.firstName;
                obj.lastName = model.lastName;
                obj.empId = model.empId;
                obj.birthDate = model.birthDate;
                obj.email = model.email;
                obj.employementDate = model.employementDate;
                obj.address = model.address;
                obj.city = model.city;
                obj.state = model.state;
                obj.country = model.country;

                if (model.empId == 0)
                {
                    dbObj.emplDetails.Add(obj);
                    dbObj.SaveChanges();

                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }
                ModelState.Clear();
            }
           
            return View("Employee");
        }


        public ActionResult EmployeeList()
        {
            var res = dbObj.emplDetails.ToList();
            return View(res);
         
        }

        [HttpGet]
        [Route("Employee/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            var res = dbObj.emplDetails.Where(x => x.empId == id).First();
            dbObj.emplDetails.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.emplDetails.ToList();

            return View("EmployeeList", list);
        }

    }
}