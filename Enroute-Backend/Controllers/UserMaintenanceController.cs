using Enroute_Backend.Contexts;
using EnrouteAppLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Enroute_Backend.Controllers
{
    
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserMaintenanceController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _db;

        public UserMaintenanceController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _db = db;
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
        [Authorize]
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


        [HttpPost]
        [Authorize]
        [ActionName("setUserLastLocation")]
        public async Task<IActionResult> setUserHistory(double latitude, double longitude )
        {
            try { 


                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                if (user != null)
                {
                    var lastLocation = _db.UserLocationHistories.Where(a => a.UserId == user.Id).OrderByDescending(a => a.Id).FirstOrDefault();


                   if (lastLocation != null)
                    {
                        if(lastLocation.Latitude == Convert.ToDecimal(latitude) && lastLocation.Longitude == Convert.ToDecimal(longitude))
                        {
                            return Ok();
                        }
                    }
                    
                   _db.UserLocationHistories.Add(new UserLocationHistory() { Latitude = Convert.ToDecimal(latitude), Longitude = Convert.ToDecimal(longitude), Date = DateTime.Now, UserId = user.Id });
                   
                     
                    

                }
                else
                {
                    return BadRequest();
                }






                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                await _db.SaveChangesAsync();
            }
        }


        [HttpGet]
        [Authorize]
        [ActionName("getUserLastLocation")]
        public async Task<IActionResult> getUserLastHistory(string email)
        {
            try
            {


                var user = await userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var lastLocation = _db.UserLocationHistories.Where(a => a.UserId == user.Id).OrderByDescending(a => a.Id).FirstOrDefault();


                    return Ok(lastLocation);

                }
                else
                {
                    return BadRequest();
                }







            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}
