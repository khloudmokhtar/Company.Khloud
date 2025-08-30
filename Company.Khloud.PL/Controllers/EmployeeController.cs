using Company.Khloud.BLL.Interfaces;
using Company.Khloud.DAL.Models;
using Company.Khloud.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Company.Khloud.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Name = model.Name,
                    Age = model.Age,
                    Salary = model.Salary,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.Phone,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted

                };

                var Count = _employeeRepository.Add(employee);

                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Details(int? id , string ViewName = "Details" )
        {
            if (id is null) return BadRequest("Invalid Id") ;
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404,Message= $"Employee with Id{id} is not Found" });

            return View (ViewName, employee);
        }

        [HttpGet]

        public IActionResult Edit (int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit ([FromRoute] int? id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();
                var Count = _employeeRepository.Update(employee);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

                
            }

            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete (int? id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id , Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();
                var Count = _employeeRepository.Delete(employee);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(employee);
        }
    } 
}

    

