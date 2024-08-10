using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1_2.Entities;
using Project1_2.Models;
using Project1_2.Services;
using System.Security.Claims;

namespace Project1_2.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        { 
            _restaurantService.Delete(id);

             return NoContent();

        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var userId = int.Parse(User.FindFirst(c=>c.Type==ClaimTypes.NameIdentifier).Value);
            var id = _restaurantService.Create(dto);
            
            return Created($"/api/restaurant/{id}",null);
        }

        [HttpGet]
        [Authorize(Policy = "Atleast2")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        { 
            var restaurants = _restaurantService.GetAll();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
           var restaurant = _restaurantService.GetById(id);

           return Ok(restaurant);
        }

        [HttpPut("{id}")]
        public ActionResult ChangeRestaurant([FromRoute] int id, [FromBody] PutRestaurantDto pdto)
        {
           
            _restaurantService.ChangeRestaurant(pdto, id);

            return Ok();
        }

       
    }
}
