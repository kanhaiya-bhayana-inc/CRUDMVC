using CRUDMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;


namespace CRUDMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private static bool IsValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        private static bool IsPhoneNbr(string number)
        {
            const string motif = @"^[0-9]{10}$";
            if (number != null) return Regex.IsMatch(number, motif);
            else return false;
        }

        private static bool IsFullName(string name)
        {
            // ([A-Z])\s[A-Z]
            // [A-Za-z]+\s[A-Za-z]
            const string regex = @"^[A-Z][a-z]*(\s[A-Z][a-z]*)+$";
            if (Regex.IsMatch(name, regex)) return true;
            return false;
        }
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
                if (String.IsNullOrEmpty(employee.Full_Name))
                {
                    ModelState.AddModelError("", "Full Name field is required!");
                    return View();
                }
                if (!IsFullName(employee.Full_Name))
                {
                    ModelState.AddModelError("", "Please enter a valid name like <John Smith>!");
                    return View();
                }
                /*if (employee.Full_Name.Any(char.IsDigit))
                {
                    ModelState.AddModelError("", "Numbers or digits are not allowed in NAME!");
                    return View();
                }*/
                if (String.IsNullOrEmpty(employee.Email))
                {
                    ModelState.AddModelError("", "Email field is required!");
                    return View();
                }
                if(!IsValid(employee.Email))
                {
                    ModelState.AddModelError("", "Please Enter valid email address!");
                    return View();
                }
                if (String.IsNullOrEmpty(employee.Phone))
                {
                    ModelState.AddModelError("", "Phone field is required!");
                    return View();
                }
               /* if (employee.Phone.Any(char.IsLetter))
                {
                    ModelState.AddModelError("", "Character or letters are not allowed in Phone!");
                    return View();
                }*/
               if (!IsPhoneNbr(employee.Phone))
                {
                    ModelState.AddModelError("", "Please enter a valid phone number!");
                    return View();
                }
                if (String.IsNullOrEmpty(employee.Address))
                {
                    ModelState.AddModelError("", "Address field is required!");
                    return View();
                }

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
