using Company.Khloud.BLL.Interfaces;
using Company.Khloud.BLL.Repositories;
using Company.Khloud.DAL.Models;
using Company.Khloud.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Khloud.PL.Controllers
{
    //MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        //ask CLR Create Object from DepartmentRepository
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet] // GET : /Departrment/Index
        public IActionResult Index()
        {
            //DepartmentRepository departmentRepository = new DepartmentRepository();
           var departments= _departmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) //Server Side Validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt

                };

                var Count = _departmentRepository.Add(department);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }


            }
            return View(model);
        }
    }
}
