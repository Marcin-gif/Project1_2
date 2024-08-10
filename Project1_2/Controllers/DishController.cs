using Microsoft.AspNetCore.Mvc;
using Project1_2.Models;
using Project1_2.Services;

namespace Project1_2.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        public DishController(IDishService dishService) 
        { 
            _dishService = dishService;
        }
        [HttpGet]
        public ActionResult GetDishes([FromRoute]int restaurantId)
        {
            var dish = _dishService.GetDish(restaurantId);

            return Ok(dish);
        }
        [HttpGet("{dishId}")]
        public ActionResult GetById([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = _dishService.GetDto(restaurantId, dishId);
            return Ok(dish);
        }
        [HttpPost]
        public ActionResult Create([FromRoute] int restaurantId, [FromBody] CreateDishDto dto)
        {
            var newDish = _dishService.Create(restaurantId, dto);

            return Created($"/api/restaurant/{restaurantId}/dish/{dto}", null);
        }
        [HttpDelete("{dishId}")]
        public ActionResult Remove(int restaurantId, int dishId)
        {
            _dishService.Remove(restaurantId, dishId);
            return NoContent();
        }
        [HttpPut("{dishId}")]
        public ActionResult Put([FromRoute] int restaurantId, [FromRoute] int dishId, [FromBody] PutDishDto dto)
        { 
            _dishService.Put(restaurantId,dishId , dto);
            return NoContent();
        }
    }
}
