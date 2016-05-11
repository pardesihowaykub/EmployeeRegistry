using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeesRegister.DataAccessLayer;
using EmployeesRegister.Models;

namespace EmployeesRegister.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeesContext db = new EmployeesContext();

        // GET: Employees
        public ActionResult Index(string searchTerm=null)
        {
            //var model =
            //    from r in db.Employees
            //    orderby r.FirstName ascending
            //    select new EmployeeListViewModel
            //    {
            //        Id = r.Id,
            //        FirstName=r.FirstName,
            //        LastName=r.LastName,
            //        Salary=r.Salary,
            //        Position=r.Position,
            //        Department=r.Department



            //    };
            var model =
                db.Employees
                .OrderByDescending(r => r.FirstName)
                .Where(r => searchTerm == null
                || r.FirstName.StartsWith(searchTerm)
                || r.LastName.StartsWith(searchTerm)
                || r.Department.StartsWith(searchTerm)
                || r.Position.StartsWith(searchTerm)
                ||r.Salary.ToString().StartsWith(searchTerm)
                );
                 //.Select(r => new Employee {

                 //    Id = r.Id,
                 //    FirstName = r.FirstName,
                 //    LastName = r.LastName,
                 //    Salary = r.Salary,
                 //    Position = r.Position,
                 //    Department = r.Department
                 //.Select(r=>new
                 //{

                 //    Id = r.Id,
                 //    FirstName = r.FirstName,
                 //    LastName = r.LastName,
                 //    Salary = r.Salary,
                 //    Position = r.Position,
                 //    Department = r.Department,
                 //    Company = r.Company



                 //});
            return View(model);
           // return View(db.Employees.ToList());
        }

        public ActionResult Sport()
        {
            var model = db.Employees.Where(i => i.Department == "Sport").ToList();
            return View(model);
        }

        public ActionResult DepartmentManagers()
        {
            var model = db.Employees.Where(i => i.Position == "Manager").ToList();
            return View(model);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Salary,Position,Department,Company")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Salary,Position,Department,Company")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
