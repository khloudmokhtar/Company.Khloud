using AutoMapper;
using Company.Khloud.BLL.Interfaces;
using Company.Khloud.DAL.Models;
using Company.Khloud.PL.Dtos;
using Company.Khloud.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Threading.Tasks;

namespace Company.Khloud.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // private readonly IEmployeeRepository _employeeRepository;
        // private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

    

        public EmployeeController(
           // IEmployeeRepository employeeRepository,
          //  IDepartmentRepository departmentRepository,
              IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;


            //  _employeeRepository = employeeRepository;
            // _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
               employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
                //Dictionary : Access this Dictionary Through 3 Properity
                //1. ViewData : Transfer Extra Data From Controller(Action) To View

                //ViewData["Message"] = "Hello From ViewData";

                //2.ViewBag   :  Transfer Extra Data From Controller(Action) To View

                // ViewBag.Message = "Hello From ViewBag";

                //3.TempData 


            return View(employees);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["Departments"] = departments;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)  //Server Side Validation
            {
                //Manual Mapping
                //var employee = new Employee()
                //{
                //    Name = model.Name,
                //    Age = model.Age,
                //    Salary = model.Salary,
                //    Email = model.Email,
                //    Address = model.Address,
                //    Phone = model.Phone,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    DepartmentId =model.DepartmentId

                //};

                if(model.Image is not null)
                {
                  model.ImageName =  DocumentSettings.UploadFile(model.Image, "Images");
                }

               var employee =  _mapper.Map<Employee>(model);

               await  _unitOfWork.EmployeeRepository.AddAsync(employee);
               var Count = await _unitOfWork.CompleteAsync();

                if (Count > 0)
                {
                    TempData["Message"] = "Employee is Created !!";
                    return RedirectToAction(nameof(Index));
                }
            }

            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["Departments"] = departments;
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id , string ViewName = "Details" )
        {
            if (id is null) return BadRequest("Invalid Id") ;
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404,Message= $"Employee with Id{id} is not Found" });


            var dto = _mapper.Map<CreateEmployeeDto>(employee); 
            return View(ViewName, dto);
           
        }

        [HttpGet]

        public async Task<IActionResult> Edit (int? id)
        {
           // var departments = _departmentRepository.GetAll();
           // ViewData["Departments"] = departments;
            if (id is null) return BadRequest("Invalid Id"); //400
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();//khloud
            ViewData["Departments"] = departments; //
            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Department With Id{id} is not found" }); 
           

            //var employeeDto = new CreateEmployeeDto()
            //{

            //    Name = employee.Name,
            //    Age = employee.Age,
            //    Salary = employee.Salary,
            //    Email = employee.Email,
            //    Address = employee.Address,
            //    Phone = employee.Phone,
            //    CreateAt = employee.CreateAt,
            //    HiringDate = employee.HiringDate,
            //    IsActive = employee.IsActive,
            //    IsDeleted = employee.IsDeleted

            //};

            var dto = _mapper.Map<CreateEmployeeDto>(employee);

            return View(dto);

          //  return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit ([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                //var employee = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.EmpName,
                //    Age = model.Age,
                //    Salary = model.Salary,
                //    Email = model.Email,
                //    Address = model.Address,
                //    Phone = model.Phone,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    DepartmentId = model.DepartmentId



                //};

                if(model.ImageName is not null && model.Image is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "Images");
                }

                if(model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");
                }


                var employee = _mapper.Map<Employee>(model);
                model.Id = id;
               _unitOfWork.EmployeeRepository.Update(employee);
                var Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    TempData["Message"] = "Employee is Updated !!";
                    return RedirectToAction(nameof(Index));
                }


            }

           // var departments = await  _unitOfWork.DepartmentRepository.GetAllAsync();
           // ViewData["Departments"] = departments;

            return View(model);
        }

        //[HttpGet]
        //public IActionResult Delete (int? id)
        //{
        //    return Details(id, "Delete");
        //}

      
           

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
           // var departments = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);//khloud
           // ViewData["Departments"] = departments;
            if (employee == null)
                return NotFound();
            var dto = _mapper.Map<CreateEmployeeDto>(employee);
            return View(dto);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete([FromRoute] int? id, Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id != employee.Id) return BadRequest();
        //        var Count = _employeeRepository.Delete(employee);
        //        if (Count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }

        //    return View(employee);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete([FromRoute] int? id, Employee employee)
        //{
        //    if (id == null)
        //        return BadRequest();

        //    if (id != employee.Id)
        //        return BadRequest();

        //    var empFromDb = _unitOfWork.EmployeeRepository.Get(id.Value);
        //    if (empFromDb == null)
        //        return NotFound();

        //   _unitOfWork.EmployeeRepository.Delete(empFromDb);
        //    var Count = _unitOfWork.Complete();


        //    if (Count > 0)
        //    {
        //        if(employee.ImageName is not null)
        //        {
        //            DocumentSettings.DeleteFile(employee.ImageName, "Images");
        //        }

        //        TempData["Message"] = "Employee deleted successfully.";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ModelState.AddModelError("", "Failed to delete employee.");
        //    return View(empFromDb);
        //}

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDto model)
        {

            var employee = _mapper.Map<Employee>(model);
            employee.Id = id;
            _unitOfWork.EmployeeRepository.Delete(employee);
            var count = await _unitOfWork.CompleteAsync();
          

            if (count > 0)
            {
                if (model.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "Images");
                }

                return RedirectToAction(nameof(Index));
            }


           

            return View(model);


        }



    }
}

    

