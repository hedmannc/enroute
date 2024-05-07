﻿using Enroute_Backend.Contexts;
using EnrouteAppLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Enroute_Backend.Controllers
{
    
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserMaintenanceController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserMaintenanceController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        [Authorize]
        [ActionName("getUserRole")]
        public async Task<IActionResult> getUserRole()
        {
            try
            { 

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

            

                var roles = await userManager.GetRolesAsync(user);

                if(roles.Any())
                {
                    return Ok(roles.First());
                }


                return Ok("Anonymos");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize("Admin,Security")]
        [ActionName("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var userviews = new List<UserViewModel>();

                var users = userManager.Users;
                
                foreach(var user in users) { 
                
                   var roles = await userManager.GetRolesAsync(user);

                    var UserModel = new UserViewModel()
                    {
                        Id = user.Id,
                        email = user.Email,
                        Role = roles.FirstOrDefault()
                    };


                    userviews.Add(UserModel);


                
                
                
                }


                return Ok(userviews);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize("Admin")]
        [ActionName("getAllRoles")]
        public async Task<IActionResult> getAllRoles()
        {
            try
            {
   

                var roles = roleManager.Roles;



                return Ok(roles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet]
        [Authorize("Admin")]
        [ActionName("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email,string roleName)
        {
            try
            {

               var user =  await userManager.FindByEmailAsync(email);


                if (user != null)
                {
                    await userManager.AddToRoleAsync(user, roleName);

                }
                else
                {
                    return BadRequest("Something went wrong check role name");
                }


                



                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}