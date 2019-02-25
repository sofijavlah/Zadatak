using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Zadatak.DTOs;
using Zadatak.Models;

namespace Zadatak.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly WorkContext context;

        public EmployeeController(WorkContext contextt)
        {
            context = contextt;
            context.Employees.Include(employee => employee.Office).Include(employee => employee.Devices);
            context.SaveChanges();

        }

        // GET: api/Employee
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = context.Employees.Include(em => em.Devices).Select(em => new EmployeeDTO
            {
                FName = em.FirstName,
                LName = em.FirstName
            });

            return Ok(employees);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public IActionResult GetEmployee(long id)
        {
            if (context.Employees.Include(e => e.Office).Include(e => e.Devices).FirstOrDefault(x => x.Id == id) == null) return NotFound();

            var targetEmployee = context.Employees.Include(e => e.Office).Include(e => e.Devices).FirstOrDefault(x => x.Id == id);
            var employee = new EmployeeDeviceListDTO
            {
                FName = targetEmployee.FirstName,
                LName = targetEmployee.LastName,
                OfficeName = targetEmployee.Office.Description,
                DeviceList = targetEmployee.Devices.Select(d => new DeviceDTO
                {
                    Name = d.Name
                })
            };
            
            return Ok(employee);

        }

        // POST: api/Employee
        [HttpPost]
        public IActionResult AddEmployeeToOffice(EmployeeToOfficeDTO e)
        {
            var broj = context.Offices.Count(o => o.Description == e.OfficeName);

            if (broj == 0)
            {
                Office o = new Office();
                o.Description = e.OfficeName;

                context.Offices.Add(o);

                context.SaveChanges();

                Employee newEmployee = new Employee();
                newEmployee.FirstName = e.FName;
                newEmployee.LastName = e.LName;
                newEmployee.OfficeId = o.Id;

                context.Employees.Add(newEmployee);

                context.SaveChanges();

                return Ok("Added Employee and Office");
            }

            var office = context.Offices.Include(o => o.Employees).First(o => o.Description == e.OfficeName);

            Employee employee = new Employee();
            employee.FirstName = e.FName;
            employee.LastName = e.LName;
            employee.OfficeId = office.Id;

            context.Employees.Add(employee);
            context.SaveChanges();

            return Ok("Added Employee");
        }

        // POST: api/Employee
        [HttpPost]
        public IActionResult AddEmployeeAndDevices(EmployeeDeviceListDTO e)
        {
            Employee newEmployee = new Employee();

            var targetOffice = context.Offices.Include(o => o.Employees).FirstOrDefault(o => o.Description == e.OfficeName);

            targetOffice.Employees.Add(newEmployee);

            context.SaveChanges();
            
            newEmployee.FirstName = e.FName;
            newEmployee.LastName = e.LName;

            newEmployee.Devices = new List<Device>();

            foreach (DeviceDTO d in e.DeviceList)
            {
                newEmployee.Devices.Add(new Device
                {
                    Name = d.Name
                });
            }

            context.SaveChanges();

            return Ok("Added Employee");
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult ChangeEmployeeName(long id, EmployeeDTO e)
        {

            var targetEmployee = context.Employees.Include(em => em.Office).FirstOrDefault(em => em.Id == id);

            if (targetEmployee == null) return NotFound("Employee doesn't exist");

            targetEmployee.FirstName = e.FName;
            targetEmployee.LastName = e.LName;

            context.SaveChanges();

            return Ok("Modified Employee name");
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult ChangeEmployeeContent(long id, EmployeeDeviceListDTO e)
        {

            var targetEmployee = context.Employees.Include(em => em.Office).FirstOrDefault(em => em.Id == id);

            if (targetEmployee == null) return NotFound("Employee doesn't exist");

            var targetOffice = context.Offices.Include(o => o.Employees).FirstOrDefault(o => o.Description == e.OfficeName);

            if (targetOffice == null)
            {
                Office newOffice = new Office
                {
                    Description = e.OfficeName
                };
                context.Offices.Add(newOffice);

                context.SaveChanges();

                targetEmployee.FirstName = e.FName;
                targetEmployee.LastName = e.LName;

                newOffice.Employees.Add(targetEmployee);

                context.SaveChanges();

                targetEmployee.Devices = new List<Device>(e.DeviceList.Select(d => new Device
                {
                    Name = d.Name
                }));

                return Ok("Changed Employee Name, made new Office and Device List");
            }
            
            if (targetOffice.Employees.Find(x => x.Id == id) != null)
            {
                targetEmployee.Devices = new List<Device>(e.DeviceList.Select(d => new Device
                {
                    Name = d.Name
                }));

                context.SaveChanges();
                return Ok("Changed Employee Device List");
            }

            var newEmployee = new Employee
            {
                FirstName = targetEmployee.FirstName,
                LastName = targetEmployee.LastName
            };

            context.Employees.Remove(targetEmployee);

            targetOffice.Employees.Add(newEmployee);

            newEmployee.FirstName = e.FName;
            newEmployee.LastName = e.LName;
            newEmployee.Devices = new List<Device>(e.DeviceList.Select(d => new Device
            {
                Name = d.Name
            }));

            context.SaveChanges();

            return Ok("Changed Employee Name, Office and Device List");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var targetEmployee = context.Employees.Include(e => e.Devices).FirstOrDefault(e => e.Id == id);

            if (targetEmployee == null) return NotFound("Employee doesn't exist");

            context.Employees.Remove(targetEmployee);

            return Ok("Deleted");
        }
    }
}
