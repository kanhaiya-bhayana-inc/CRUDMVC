using CRUDMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            using (EmployeeDbEntities db = new EmployeeDbEntities())
            {
                List<EmployeeTbl> employeeList = new List<EmployeeTbl>(from data in db.EmployeeTbls
                                                                       select data).ToList();
                return View(employeeList);
            }
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View(new EmployeeTbl());
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmployeeTbl employee)
        {
            try
            {

                // TODO: Add insert logic here 
                using (EmployeeDbEntities db = new EmployeeDbEntities())
                {
                    db.EmployeeTbls.Add(employee);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            using (EmployeeDbEntities db = new EmployeeDbEntities())
            {
                EmployeeTbl employeeTbl = db.EmployeeTbls.Find(id);
                return View(employeeTbl);
            }
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmployeeTbl employee)
        {
            try
            {
                // TODO: Add update logic here
                using (EmployeeDbEntities db = new EmployeeDbEntities())
                {
                    EmployeeTbl emp = db.EmployeeTbls.Find(employee.Id);
                    emp.Full_Name = employee.Full_Name;
                    emp.Email = employee.Email;
                    emp.Phone = employee.Phone;
                    emp.Address = employee.Address;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            using (EmployeeDbEntities db = new EmployeeDbEntities())
            {
                db.EmployeeTbls.Remove(db.EmployeeTbls.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
