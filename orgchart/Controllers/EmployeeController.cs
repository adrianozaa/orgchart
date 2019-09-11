using orgchart.Models;
using orgchart.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace orgchart.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index(int? ID)
        {
            EmployeeContext employeeContext = new EmployeeContext();

            var vm = new EmployeesVM();
            var employees = employeeContext.Employees.ToList();
            if (ID.HasValue && ID.Value >= 0)
            {
                vm.Employees = employees.Where(x => x.DepartmentID == ID).ToList();
                vm.SelectedDepartment = ID.Value;
            }
            else
            {
                vm.Employees = employees;
            }
            vm.Roles = employeeContext.Roles.ToList();
            vm.EmployeeRoles = employeeContext.EmployeeRoles.ToList();
            var managers = new List<Employee>();
            foreach (var e in employees.GroupBy(x => x.ReportsToID))
            {
                var manager = employees.FirstOrDefault(x => x.ID == e.Key);
                if (manager != null)
                {
                    managers.Add(manager);
                }
            }
            vm.Managers = managers;
            vm.Departments = employeeContext.Departments;
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
            var vm = new EmployeeVM();
            vm.Employees = employeeContext.Employees.ToList();
            vm.Employee = vm.Employees.FirstOrDefault(x => x.ID == ID);
            vm.Roles = employeeContext.Roles.ToList();
            vm.EmployeeRoles = employeeContext.EmployeeRoles.Where(x => x.EmployeeID == ID).Select(x => x.RoleID).ToList();
            vm.Departments = employeeContext.Departments.ToList();

            ViewBag.Roles = new MultiSelectList(vm.Roles, "ID", "Description");
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            EmployeeContext employeeContext = new EmployeeContext();
            employeeContext.Entry(employee).State = System.Data.Entity.EntityState.Modified;
            employeeContext.SaveChanges();

            //update roles
            var employeeCurrentRoles = employeeContext.EmployeeRoles.Where(x => x.EmployeeID == employee.ID).ToList();
            //remove roles that are no longer our our list
            foreach (var role in employeeCurrentRoles.Where(x => !employee.Roles.Any(y => y == x.RoleID)).ToList())
            {
                employeeContext.EmployeeRoles.Remove(role);
                employeeContext.SaveChanges();
            }
            //add any new roles that are not in the new Role list
            foreach(var newRoleID in employee.Roles.Where(x=>!employeeCurrentRoles.Any(y=>y.RoleID == x)).ToList())
            {
                var newEmployeeRole = new EmployeeRole() { RoleID = newRoleID, EmployeeID = employee.ID };
                employeeContext.EmployeeRoles.Add(newEmployeeRole);
                employeeContext.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            //ViewBag.Title = "Edit Page";
            EmployeeContext employeeContext = new EmployeeContext();
            var employee = employeeContext.Employees.FirstOrDefault(x => x.ID == ID);
            //vm.Departments = employeeContext.Departments.ToList();

            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int ID, Employee employee)
        {
            EmployeeContext employeeContext = new EmployeeContext();
            employeeContext.Employees.Remove(employeeContext.Employees.FirstOrDefault(x => x.ID == ID));
            //remove all employee roles
            var employeeRoles = employeeContext.EmployeeRoles.Where(x => x.EmployeeID == ID).ToList();
            foreach (var role in employeeRoles)
            {
                employeeContext.EmployeeRoles.Remove(role);
                employeeContext.SaveChanges();
            }
            employeeContext.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            EmployeeContext employeeContext = new EmployeeContext();
            employeeContext.Employees.Add(employee);
            employeeContext.SaveChanges();
            //save roles
            foreach (var newRoleID in employee.Roles)
            {
                var newEmployeeRole = new EmployeeRole() { RoleID = newRoleID, EmployeeID = employee.ID };
                employeeContext.EmployeeRoles.Add(newEmployeeRole);
                employeeContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            EmployeeContext employeeContext = new EmployeeContext();
            var vm = new EmployeeVM();
            vm.Employees = employeeContext.Employees.ToList();
            //vm.Employee = vm.Employees.FirstOrDefault(x => x.ID == ID);
            vm.Roles = employeeContext.Roles.ToList();
            //vm.EmployeeRoles = employeeContext.EmployeeRoles.Where(x => x.EmployeeID == ID).Select(x => x.RoleID).ToList();
            vm.Departments = employeeContext.Departments.ToList();

            ViewBag.Roles = new MultiSelectList(vm.Roles, "ID", "Description");
            return View(vm);
        }

    }
}