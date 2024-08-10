using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1_2.Entities;
using Project1_2.Exceptions;
using Project1_2.Models;

namespace Project1_2.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DishService> _logger;

        public DishService(RestaurantDbContext dbContext, IMapper mapper, ILogger<DishService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantId(restaurantId);
            var entityDish = _mapper.Map<Dish>(dto);
            if (restaurant == null)
            {
                throw new NotFoundException("Restaurant not found");
            }
           entityDish.RestaurantId = restaurantId;
            _dbContext.dishes.Add(entityDish);
            _dbContext.SaveChanges();

            return entityDish.Id;
                
        }

        public List<DishDto> GetDish(int id)
        {
            var restaurant = GetRestaurantId(id);

            var dishDto = _mapper.Map<List<DishDto>>(restaurant.Dishes);
            return dishDto;
        }
        
        public DishDto GetDto(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantId(restaurantId);
            var dish = _dbContext.dishes.FirstOrDefault(d=>d.Id==dishId);

            if (dish is null || dish.RestaurantId != restaurantId)
                throw new NotFoundException("Dish not found");

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }

        public void Put(int restaurantId, int dishId, PutDishDto dto)
        {
            var restaurant = GetRestaurantId(restaurantId);
            var dish = GetDishTmp(dishId);
            //if (!string.IsNullOrEmpty(dto.Name))
            //{ 
            //    dish.Name = dto.Name;
            //}
            //if (!string.IsNullOrEmpty(dto.Description))
            //{ 
            //    dish.Description = dto.Description;
            //}
            //if (dto.Price != null)
            //{ 
            //    dish.Price = dto.Price;
            //}
            
            var dishEntity = _mapper.Map(dto, dish);
            dishEntity.RestaurantId = restaurantId;
            _dbContext.dishes.Update(dishEntity);
            _dbContext.SaveChanges();
        }

        public void Remove(int restaurantId, int dishId)
        {
            _logger.LogError($"Restaurant with id {restaurantId} and dish {dishId} Delete action invoked");
            var restaurant = GetRestaurantId(restaurantId);
            var dish = _dbContext.dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish is null || dish.RestaurantId != restaurantId)
                throw new NotFoundException("dish not found");
            _dbContext.dishes.Remove(dish);
            _dbContext.SaveChanges();
        }
        private Dish GetDishTmp(int dishId)
        { 
            var dish = _dbContext.dishes.FirstOrDefault(d=>d.Id==dishId);
            if (dish is null)
                throw new NotFoundException("Dish not found");
            return dish;
        }
        private Restaurant GetRestaurantId(int restaurantId)
        {
            var restaurant = _dbContext
                .restaurants
                .Include(r=>r.Dishes)
                .FirstOrDefault(d=>d.Id==restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            return restaurant;
        }
    }
}
