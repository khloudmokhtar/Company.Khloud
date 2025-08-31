using Company.Khloud.BLL.Interfaces;
using Company.Khloud.BLL.Repositories;
using Company.Khloud.DAL.Models;
using Company.Khloud.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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

        [HttpGet]
        public IActionResult Details(int? id, string ViewName="Details" )
        {
            if (id is null) return BadRequest("Invalid Id"); //400
           var department =  _departmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id{id} is not found" });


            return View(ViewName,department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id"); //400
            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id{id} is not found" });


            //return Details(id,"Edit");



            if (id is null) return BadRequest("Invalid Id"); //400
            var department = _departmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id{id} is not found" });

            var departmentDto = new CreateDepartmentDto()
            {
                Code = department.Code,
                Name = department.Name,
               CreateAt= department.CreateAt
               
            };

            return View(departmentDto);


        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, Department department)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id != department.Id) return BadRequest(); //404
        //        var Count = _departmentRepository.Update(department);
        //        if (Count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }


        //    return View(department);
        //}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id = id,
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt
                };

                var Count = _departmentRepository.Update(department);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }


            return View(model);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id"); //400
            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id{id} is not found" });


            return Details(id,"Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest(); //404
                var Count = _departmentRepository.Delete(department);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }


            return View(department);
        }

    }
}
