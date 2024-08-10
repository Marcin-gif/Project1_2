using Project1_2.Models;

namespace Project1_2.Services
{
    public interface IDishService
    {
        List<DishDto> GetDish(int id);
        DishDto GetDto(int restaurantId, int dishId);
        int Create(int restaurantId, CreateDishDto dto);
        public void Remove(int restaurantId, int dishId);
        public void Put(int restaurantId, int dishId, PutDishDto dto);
    }
}