using orgchart.Models;
using orgchart.ViewModels.Department;
using orgchart.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace orgchart.Controllers
{
    public class DepartmentController : Controller
    {
        public ActionResult Index()
        {
            EmployeeContext employeeContext = new EmployeeContext();

            var vm = new DepartmentsVM();
            var departments = employeeContext.Departments.ToList();
            vm.Departments = departments.ToList();

            return View(vm);
        }

        public ActionResult OrgChart()
        {
            EmployeeContext employeeContext = new EmployeeContext();
            var cnt = employeeContext.Employees.Count();

            return View(employeeContext.Employees);
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            ViewBag.Title = "Edit Page";
            EmployeeContext employeeContext = new EmployeeContext();
            var vm = new DepartmentVM();
            vm.Department = employeeContext.Departments.FirstOrDefault(x => x.ID == ID);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            EmployeeContext employeeContext = new EmployeeContext();
            employeeContext.Entry(department).State = System.Data.Entity.EntityState.Modified;
            employeeContext.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            //ViewBag.Title = "Edit Page";
            EmployeeContext employeeContext = new EmployeeContext();
            var department = employeeContext.Departments.FirstOrDefault(x => x.ID == ID);
            return View(department);
        }

        [HttpPost]
        public ActionResult Delete(int ID, Department department)
        {
            EmployeeContext employeeContext = new EmployeeContext();
            employeeContext.Departments.Remove(employeeContext.Departments.FirstOrDefault(x => x.ID == ID));
            //remove all employee roles
            //var employeeRoles = employeeContext.EmployeeRoles.Where(x => x.EmployeeID == ID).ToList();
            //foreach (var role in employeeRoles)
            //{
            //    employeeContext.EmployeeRoles.Remove(role);
            //    employeeContext.SaveChanges();
            //}
            employeeContext.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            EmployeeContext employeeContext = new EmployeeContext();
            //int nextID = employeeContext.Departments.Max(x => x.ID) + 1;
            //department.ID = nextID;

            employeeContext.Departments.Add(department);
            employeeContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            EmployeeContext employeeContext = new EmployeeContext();
            var vm = new DepartmentVM();

            return View(vm);
        }

    }
}