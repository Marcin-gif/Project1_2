using Project1_2.Entities;
using Project1_2.Models;
using System.Security.Claims;

namespace Project1_2.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        void Delete(int id);
        void ChangeRestaurant(PutRestaurantDto pdto, int id);
        public Task<IEnumerable<Restaurant>> GetRestaurantsCreatedByUser(int userId);
    }
}