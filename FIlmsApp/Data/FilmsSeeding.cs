using FIlmsApp.Models.Entities;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;

namespace FIlmsApp.Data
{
    public static class FilmsSeeding
    {
        public static void Seed(ModelBuilder builder)
        {
            Random rnd = new Random();
            builder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action"},
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Horror" },
                new Genre { Id = 4, Name = "Thriller" },
                new Genre { Id = 5, Name = "Drama" },
                new Genre { Id = 6, Name = "Adventure" }
                );

            var random = new RandomGenerator();

            var films = Builder<Film>.CreateListOfSize(500)
                .All()
                .With(m => m.Name = Faker.Name.FullName())
                .With(m => m.GenreId = rnd.Next(1, 6))
                .With(m => m.ReleasedDate = DateTime.Now.AddYears(-random.Next(0, 50)))
                .Build();

            builder.Entity<Film>().HasData(films);


            var actors = Builder<Actor>.CreateListOfSize(500)
             .All()
             .With(a => a.Name = Faker.Name.FullName())
             .Build();


            builder.Entity<Actor>().HasData(actors);

            var pivotList = Builder<ActorFilm>.CreateListOfSize(100)
                .All()
                .With(m => m.ActorsId = rnd.Next(1, 500))
                .With(x => x.FilmsId = rnd.Next(1, 500))
                .Build();

            builder.Entity<Actor>()
           .HasMany(p => p.Films)
           .WithMany(t => t.Actors)
           .UsingEntity<Dictionary<string, object>>(
               "ActorFilm",
               r => r.HasOne<Film>().WithMany().HasForeignKey("FilmsId"),
               l => l.HasOne<Actor>().WithMany().HasForeignKey("ActorsId"),
               je =>
               {
                   je.HasKey("ActorsId", "FilmsId");
                   je.HasData(pivotList);
               });
        }
    }
}
