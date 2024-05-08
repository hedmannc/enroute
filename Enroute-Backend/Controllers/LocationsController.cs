using Enroute_Backend.Contexts;
using EnrouteAppLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Enroute_Backend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {

        public readonly ApplicationDbContext _applicationDbContext;
        public LocationsController( ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]

        [ActionName("getlocations")]
        public async Task<IActionResult> GetLocations()
        {

            try
            {
                var db = _applicationDbContext;

                var locations =await db.Locations.ToListAsync();



                return Ok(locations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           




        }


        [HttpGet]

        [ActionName("getbuilding")]
        public async Task<IActionResult> GetBuilding(int buildingid)
        {

            try
            {
                var db = _applicationDbContext;

                var building = await db.Buildings.Where(a => a.Id == buildingid).FirstAsync();



                return Ok(building);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }





        }


        [HttpGet]
        
        [ActionName("GetBuildings")]
        public async Task<IActionResult> GetBuildings()
        {

            try
            {
                var db = _applicationDbContext;

                var buildings = await db.Buildings.ToListAsync();



                return Ok(buildings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }





        }




        [HttpPost]
        [Authorize("Admin")]
        [ActionName("AddBuilding")]
        public async Task<IActionResult> AddBuilding(Building building)
        {

            try
            {
                var db = _applicationDbContext;

                await db.Buildings.AddAsync(building);

                await db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }



        [HttpPost]
        [Authorize("Admin")]
        [ActionName("AddLocation")]
        public async Task<IActionResult> AddLocation(Location location)
        {

            try
            {
                var db = _applicationDbContext;

               await db.Locations.AddAsync(location);

                await db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

    }
}
