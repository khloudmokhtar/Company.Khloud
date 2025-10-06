using Company.Khloud.BLL.Interfaces;
using Company.Khloud.BLL.Repositories;
using Company.Khloud.DAL.Models;
using Company.Khloud.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Threading.Tasks;

namespace Company.Khloud.PL.Controllers
{

    [Authorize]
    //MVC Controller
    public class DepartmentController : Controller
    {
        // private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        //ask CLR Create Object from DepartmentRepository
        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
        {
            // _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet] // GET : /Departrment/Index
        public async Task<IActionResult> Index()
        {
            //DepartmentRepository departmentRepository = new DepartmentRepository();
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) //Server Side Validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt

                };

                await  _unitOfWork.DepartmentRepository.AddAsync(department);
                var Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }


            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id{id} is not found" });


            return View(ViewName, department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id"); //400
            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id{id} is not found" });


            //return Details(id,"Edit");



            if (id is null) return BadRequest("Invalid Id"); //400
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id{id} is not found" });

            var departmentDto = new CreateDepartmentDto()
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt

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
        public async Task<IActionResult> Edit([FromRoute] int id, UpdateDepartmentDto model)
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

                _unitOfWork.DepartmentRepository.Update(department);
                var Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }


            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department == null) return NotFound(new { StatusCode = 404, Message = $"Department With Id{id} is not found" });
            return View(department);
        }


        // [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        // public IActionResult DeleteConfirmed(/*[FromRoute] int id, Department department*/ int id)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var department = _unitOfWork.DepartmentRepository.Get(id);
        //         if (department == null) return NotFound(); //404
        //        _unitOfWork.DepartmentRepository.Delete(department);
        //         var Count = _unitOfWork.Complete();
        //         if (Count > 0)
        //         {
        //             return RedirectToAction(nameof(Index));
        //         }
        //     }


        //     return View(department);
        // }



        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public IActionResult Delete(int id)
        //    {
        //        var department = _unitOfWork.DepartmentRepository.Get(id);

        //        if (department == null)
        //            return NotFound();

        //        _unitOfWork.DepartmentRepository.Delete(department);
        //        var count = _unitOfWork.Complete();

        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }



        //        return View(department);
        //    }

        //}



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);

            if (department == null)
                return NotFound();

            _unitOfWork.DepartmentRepository.Delete(department);
            var count = await _unitOfWork.CompleteAsync();

            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }



            return View(department);
        }
    }
}
