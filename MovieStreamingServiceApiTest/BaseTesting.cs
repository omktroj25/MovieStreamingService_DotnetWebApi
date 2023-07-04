using Entity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Entity.Model;
using Service;
using Repository;
using Contract.IService;
using Contract.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MovieStreamingServiceApi;
using MovieStreamingServiceApi.Controller;
using Microsoft.Extensions.DependencyInjection;
using Entity;

namespace MovieStreamingServiceApiTest
{
    public class BaseTesting
    {
        public readonly AdminController adminController;
        public readonly AuthenticationController authenticationController;
        public readonly MovieController movieController;
        public readonly SubscriptionController subscriptionController;
        public readonly UserController userController;
        public readonly IConfiguration _config;
        public readonly ApiDbContext _context;
        private readonly DbContextOptionsBuilder<ApiDbContext> optionsBuilder;
        public readonly IMapper _mapper;

        public static Guid userId = Guid.NewGuid();
        public static Guid userId2 = Guid.NewGuid();
        public static string name = "AdminUser";
        public static string name2 = "UserUser";

        public BaseTesting()
        {

            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new ApiDbContext(optionsBuilder.Options);
            _context.SaveChanges();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
            _mapper = mapperConfiguration.CreateMapper();


            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            Guid subscription1 = Guid.NewGuid();
            Guid subscription2 = Guid.NewGuid();
            Guid subscription3 = Guid.NewGuid();
            Guid subscription4 = Guid.NewGuid();

            User[] users = new User[]
            {
                new User{ Id = userId , UserName = name , Password = "/3vZexp3id3Sd1Ei/WgX8xc2ctqfgCzuxX8oQyW/WJ8=" , Role = "Admin" }, //Pasword@123
                new User{ Id = userId2, UserName = name2, Password = "dM0p8PMqmscp69xac484T6OErIqk5WM4qDtV+MzVGdY=" , Role = "User"}, //Propel@123
            };
            _context.Users.AddRange(users);
            _context.SaveChanges();

            Entity.Model.Profile[] profiles = new Entity.Model.Profile[]
            {
                new Entity.Model.Profile{ Id = Guid.NewGuid(), UserId = userId2, Email = "user@outlook.com", PhoneNumber = "+919876543210", SubscriptionId = subscription3, CreatedBy = userId2, CreatedOn = DateTime.UtcNow, UpdatedBy = userId2, UpdatedOn = DateTime.UtcNow }
            };
            _context.Profiles.AddRange(profiles);
            _context.SaveChanges();

            UpiPayment[] upiPayments = new UpiPayment[]
            {
                new UpiPayment{ Id = Guid.NewGuid(), UserId = userId2, PaymentType = "UPI", UpiId = "user@bank", UpiApp = "gpay", CreatedBy = userId2, CreatedOn = DateTime.UtcNow, UpdatedBy = userId2, UpdatedOn = DateTime.UtcNow }
            };
            _context.UpiPayments.AddRange(upiPayments);
            _context.SaveChanges();

            CardPayment[] cardPayments = new CardPayment[]
            {
                new CardPayment{ Id = Guid.NewGuid(), UserId = userId2, PaymentType = "CREDIT/CARD", CardNumber = "12345678909876", CardHolderName = "User", ExpireDate= "01/25" , CreatedBy = userId2, CreatedOn = DateTime.UtcNow, UpdatedBy = userId2, UpdatedOn = DateTime.UtcNow }
            };
            _context.CardPayments.AddRange(cardPayments);
            _context.SaveChanges();

            Subscription[] subscriptions = new Subscription[]
            {
                new Subscription{
                    Id = subscription1,
                    Key = "BASIC",
                    Description = "This plan offers standard definition (SD) streaming on one device at a time. It's a cost-effective option for individuals or budget-conscious users",
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Subscription{
                    Id = subscription2,
                    Key = "STANDARD",
                    Description = "This plan provides high definition (HD) streaming on up to two devices simultaneously. It is suitable for users who prefer better video quality and want to share their account with family members",
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Subscription{
                    Id = subscription3,
                    Key = "PREMIUM",
                    Description = "This plan offers Ultra HD (4K) streaming on up to four devices at the same time. It is ideal for users with large households or those who desire the best video quality available",
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Subscription{
                    Id = subscription4,
                    Key = "FREE",
                    Description = "The free plan offers limited access to the streaming site's content library",
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                }
            };
            _context.Subscriptions.AddRange(subscriptions);
            _context.SaveChanges();

            Movie[] movies = new Movie[]
            {
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "The Shape of Water",
                    Genere = "Fantasy",
                    Director = "Guillermo del Toro",
                    Actor = "Sally Hawkins",
                    Rating = 4.2M,
                    SubscriptionId = subscription1,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Moonlight",
                    Genere = "Drama",
                    Director = "Barry Jenkins",
                    Actor = "Mahershala Ali",
                    Rating = 4.1M,
                    SubscriptionId = subscription1,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Spotlight",
                    Genere = "Drama",
                    Director = "Tom McCarthy",
                    Actor = "Michael Keaton",
                    Rating = 4.4M,
                    SubscriptionId = subscription1,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Parasite",
                    Genere = "Thriller",
                    Director = "Bong Joon Ho",
                    Actor = "Song Kang Ho",
                    Rating = 4.3M,
                    SubscriptionId = subscription1,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "The Lord of the Rings: The Return of the King",
                    Genere = "Fantasy",
                    Director = "Peter Jackson",
                    Actor = "Elijah Wood",
                    Rating = 4.5M,
                    SubscriptionId = subscription2,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "The Godfather",
                    Genere = "Crime",
                    Director = "Francis Ford Coppola",
                    Actor = "Marlon Brando",
                    Rating = 4.7M,
                    SubscriptionId = subscription2,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Gone with the Wind",
                    Genere = "Drama",
                    Director = "Victor Fleming",
                    Actor = "Clark Gable",
                    Rating = 4.2M,
                    SubscriptionId = subscription2,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Schindler's List",
                    Genere = "Biography",
                    Director = "Steven Spielberg",
                    Actor = "Liam Neeson",
                    Rating = 4.6M,
                    SubscriptionId = subscription2,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Casablanca",
                    Genere = "Drama",
                    Director = "Michael Curtiz",
                    Actor = "Humphrey Bogart",
                    Rating = 4.4M,
                    SubscriptionId = subscription3,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "The Departed",
                    Genere = "Crime",
                    Director = "Martin Scorsese",
                    Actor = "Leonardo DiCaprio",
                    Rating = 4.5M,
                    SubscriptionId = subscription3,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "The Silence of the Lambs",
                    Genere = "Thriller",
                    Director = "Jonathan Demme",
                    Actor = "Anthony Hopkins",
                    Rating = 4.4M,
                    SubscriptionId = subscription3,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Birdman",
                    Genere = "Drama",
                    Director = "Alejandro González Iñárritu",
                    Actor = "Michael Keaton",
                    Rating = 4.2M,
                    SubscriptionId = subscription3,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "12 Years a Slave",
                    Genere = "Drama",
                    Director = "Steve McQueen",
                    Actor = "Chiwetel Ejiofor",
                    Rating = 4.3M,
                    SubscriptionId = subscription4,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "No Country for Old Men",
                    Genere = "Thriller",
                    Director = "Joel Coen, Ethan Coen",
                    Actor = "Javier Bardem",
                    Rating = 4.5M,
                    SubscriptionId = subscription4,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "The King's Speech",
                    Genere = "Drama",
                    Director = "Tom Hooper",
                    Actor = "Colin Firth",
                    Rating = 4.1M,
                    SubscriptionId = subscription4,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "A Beautiful Mind",
                    Genere = "Biography",
                    Director = "Ron Howard",
                    Actor = "Russell Crowe",
                    Rating = 4.3M,
                    SubscriptionId = subscription4,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                }
            };
            _context.Movies.AddRange(movies);
            _context.SaveChanges();


            adminController = new AdminController(_config, _context, _mapper);
            authenticationController = new AuthenticationController(_config, _context, _mapper);
            movieController = new MovieController(_config, _context, _mapper);
            subscriptionController = new SubscriptionController(_config, _context, _mapper);
            userController = new UserController(_config, _context, _mapper);
        }
    }
}