using DataBaseApp.Data;
using DataBaseApp.Models;
using DataBaseApp.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DataBaseApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoContext mvcCDemoContext;
        public EmployeesController(MVCDemoContext mVCDemoContext)
        {
            this.mvcCDemoContext = mVCDemoContext;
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,

            };
            await mvcCDemoContext.Employees.AddAsync(employee);
            await mvcCDemoContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcCDemoContext.Employees.ToListAsync();
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee=await mvcCDemoContext.Employees.FirstOrDefaultAsync(x=>x.Id == id);
            
            if(employee!=null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                    
                };
                return await Task.Run(()=> View("View",viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcCDemoContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;
                await mvcCDemoContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult>Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcCDemoContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                mvcCDemoContext.Employees.Remove(employee);
                await mvcCDemoContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

    }


}
