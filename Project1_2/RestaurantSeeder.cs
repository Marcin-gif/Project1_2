using Project1_2.Entities;

namespace Project1_2
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        private IEnumerable<Role> GetRoles()
        { 
            var roles = new List<Role>()
            { 
                new Role()
                {
                    Name = "User"
                },
                new Role()
                { 
                    Name="Manager"
                },
                new Role()
                { 
                    Name="Admin"
                }
            };   
            return roles;
        }

        public RestaurantSeeder(RestaurantDbContext dbContext)
        { 
            _dbContext = dbContext;
        }

        public RestaurantSeeder()
        {
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.restaurants.Any())
                {
                    var rst = GetRestaurants();
                    _dbContext.restaurants.AddRange(rst);
                    _dbContext.SaveChanges();
                }
            }
        }
        public IEnumerable<Restaurant> GetRestaurants() 
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description="sdafdasf",
                    ContactNumber = "432-243-323",
                    ContactEmail="contact@sadad",
                    HasDelivery=true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Description="adasadas",
                            Price = 10.32M,
                        },
                        new Dish()
                        { 
                            Name = "Chicken Nuggets",
                            Description="dfgfdhdf",
                            Price = 5.32M,
                        },
                        
                    },
                    Address = new Address()
                    { 
                        City="Kraków",
                        Street="Długa 5",
                        PostalCode="30-003"
                    }
                    
                },
                new Restaurant()
                {
                    Name = "McDonald",
                    Category = "Fast Food",
                    Description="asfasfas",
                    ContactNumber = "432-243-323",
                    ContactEmail="contact@mcdonald",
                    HasDelivery=true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "McChicken",
                            Description="adasadas",
                            Price = 21.32M,
                        },
                        new Dish()
                        {
                            Name = "Big mac",
                            Description="adasadas",
                            Price = 11.32M,
                        },

                    },
                    Address = new Address()
                    {
                        City="Warszawa",
                        Street="Szeroki 23",
                        PostalCode="10-212"
                    }
                }
            };
            return restaurants;
        }
    }
}
