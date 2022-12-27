using FIlmsApp.Models.Entities;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using System;

namespace FIlmsApp.Data
{
    public static class FilmsSeeding
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action"},
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Horror" },
                new Genre { Id = 4, Name = "Thriller" },
                new Genre { Id = 5, Name = "Drama" },
                new Genre { Id = 6, Name = "Adventure" }
                );

            var random = new RandomGenerator();

            var films = Builder<Film>.CreateListOfSize(100)
                .All()
                .With(m => m.Name = Faker.Name.FullName())
                .With(m => m.GenreId = Faker.RandomNumber.Next(1,6))
                .With(m => m.ReleasedDate = DateTime.Now.AddYears(-random.Next(0, 50)))
                .Build();

            builder.Entity<Film>().HasData(films);


            var actors = Builder<Actor>.CreateListOfSize(100)
             .All()
             .With(a => a.Name = Faker.Name.FullName())
             .With(a => a.Films = new List<Film> {films.ElementAt(Faker.RandomNumber.Next(1, 100)), films.ElementAt(Faker.RandomNumber.Next(1, 100)) })
             .Build();


            builder.Entity<Actor>().HasData(actors);
        }
    }
}
