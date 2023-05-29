using Microsoft.EntityFrameworkCore;
using Entity.Model;

namespace Entity.Data
{
    public class ApiDbContext : DbContext
    {

        public virtual DbSet<CardPayment> CardPayments{get;set;} = null!;
        public virtual DbSet<Movie> Movies{get;set;} = null!;
        public virtual DbSet<Profile> Profiles{get;set;} = null!;
        public virtual DbSet<Subscription> Subscriptions{get;set;} = null!;
        public virtual DbSet<UpiPayment> UpiPayments{get;set;} = null!;
        public virtual DbSet<User> Users{get;set;} = null!;
      
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid userId1 = Guid.NewGuid();
            Guid userId2 = Guid.NewGuid();
            Guid subscription1 = Guid.NewGuid();
            Guid subscription2 = Guid.NewGuid();
            Guid subscription3 = Guid.NewGuid();
            Guid subscription4 = Guid.NewGuid();

            //Seeding data in the user table
            modelBuilder.Entity<User>().HasData(
                new User{
                    Id = userId1,
                    UserName = "AdminUser",
                    Password = "/3vZexp3id3Sd1Ei/WgX8xc2ctqfgCzuxX8oQyW/WJ8=", // Password@123
                    Role = "Admin",
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
                },
                new User{
                    Id = userId2,
                    UserName = "Propel",
                    Password = "dM0p8PMqmscp69xac484T6OErIqk5WM4qDtV+MzVGdY=", // Propel@123
                    Role = "Admin",
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
                }
            );

            //Seeding data in the subscription table
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription{
                    Id = subscription1,
                    Key = "BASIC",
                    Description = "This plan offers standard definition (SD) streaming on one device at a time. It's a cost-effective option for individuals or budget-conscious users",
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
                },
                new Subscription{
                    Id = subscription2,
                    Key = "STANDARD",
                    Description = "This plan provides high definition (HD) streaming on up to two devices simultaneously. It is suitable for users who prefer better video quality and want to share their account with family members",
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
                },
                new Subscription{
                    Id = subscription3,
                    Key = "PREMIUM",
                    Description = "This plan offers Ultra HD (4K) streaming on up to four devices at the same time. It is ideal for users with large households or those who desire the best video quality available",
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
                },
                new Subscription{
                    Id = subscription4,
                    Key = "FREE",
                    Description = "The free plan offers limited access to the streaming site's content library",
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
                }
            );

            //Sedding data in the movie table
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "The Shape of Water",
                    Genere = "Fantasy",
                    Director = "Guillermo del Toro",
                    Actor = "Sally Hawkins",
                    Rating = 4.2M,
                    SubscriptionId = subscription1,
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
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
                    CreatedBy = userId1,
                    UpdatedBy = userId1,
                }
            );

        }

    }    
}