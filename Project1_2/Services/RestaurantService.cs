using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Project1_2.Authorization;
using Project1_2.Entities;
using Project1_2.Exceptions;
using Project1_2.Models;
using System.Security.Claims;

namespace Project1_2.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper
            ,ILogger<RestaurantService> logger,IAuthorizationService authorizationService,IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

       

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant Not Found");

            var result = _mapper.Map<RestaurantDto>(restaurant);

            return result;

        }
        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                .restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantsDtos;
        }
        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = _userContextService.GetUserId;
            _dbContext.restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }
        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with id: {id} Delete action invoked");
            var restaurant = _dbContext
                .restaurants
                .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) throw new NotFoundException("Restaurant Not Found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, 
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
            
        }
        public void ChangeRestaurant(PutRestaurantDto pdto, int id)
        {
            
            var restaurant1 = _dbContext.restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant1 is null) throw new NotFoundException("Restaurant Not Found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant1,new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            var restaurant = _mapper.Map(pdto,restaurant1);

            _dbContext.restaurants.Update(restaurant);
            _dbContext.SaveChanges();
                
            
        }
        public async Task<IEnumerable<Restaurant>> GetRestaurantsCreatedByUser(int userId)
        {
            return await _dbContext.restaurants
                .Where(r => r.CreatedById == userId).ToListAsync();
        }
    }
}
