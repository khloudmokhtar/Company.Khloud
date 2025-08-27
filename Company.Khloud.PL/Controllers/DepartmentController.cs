using Company.Khloud.BLL.Interfaces;
using Company.Khloud.BLL.Repositories;
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
    }
}
