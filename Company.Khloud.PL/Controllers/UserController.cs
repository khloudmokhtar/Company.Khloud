using Company.Khloud.DAL.Models;
using Company.Khloud.PL.Dtos;
using Company.Khloud.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Company.Khloud.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDto> Users;

            if (string.IsNullOrEmpty(SearchInput))
            {
                  Users = _userManager.Users.Select(U=> new UserToReturnDto()
                    {

                      Id = U.Id,
                      UserName = U.UserName,
                      FirstName = U.FirstName,
                      LastName = U.LastName,
                      Email = U.Email,
                      Roles = _userManager.GetRolesAsync(U).Result

                  });
                    
                
            }
            else
            {
                Users = _userManager.Users.Select(U => new UserToReturnDto()
                {

                    Id = U.Id,
                    UserName = U.UserName,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result

                }).Where(U=>U.FirstName.ToLower().Contains(SearchInput.ToLower()));
            }

             



            return View(Users);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new { StatusCode = 404, Message = $"User with Id{id} is not Found" });


            var dto = new UserToReturnDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };

            return View(ViewName, dto);
        }


        [HttpGet]

        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operation !");
                var user =   await _userManager.FindByIdAsync(id);
                if(user is null) return BadRequest("Invalid Operation !");
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;


                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id, UserToReturnDto model)
        {



            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operation !");
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return BadRequest("Invalid Operation !");
               
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }


            return View(model);


        }



    }
}

