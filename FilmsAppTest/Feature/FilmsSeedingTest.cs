using FIlmsApp.Data;
using FIlmsApp.Models.Entities;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsAppTest.Feature
{
    public static class FilmsSeedingTest
    {
        public static void SeedFilms(FilmsContext filmsContext)
        {
            filmsContext.Genres.RemoveRange(filmsContext.Genres);
            filmsContext.Actors.RemoveRange(filmsContext.Actors);
            filmsContext.Films.RemoveRange(filmsContext.Films);
            //var context = new FilmsContext(filmsContext);

            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            filmsContext.Genres.AddRange(new Genre { Id = 1, Name = "Action" },
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
                .With(m => m.GenreId = Faker.RandomNumber.Next(1, 6))
                .With(m => m.ReleasedDate = DateTime.Now.AddYears(-random.Next(0, 50)))
                .Build();

            filmsContext.Films.AddRange(films);


            var actors = Builder<Actor>.CreateListOfSize(100)
             .All()
             .With(a => a.Name = Faker.Name.FullName())
             .With(a => a.Films = new List<Film> { films.ElementAt(Faker.RandomNumber.Next(0, 99)), films.ElementAt(Faker.RandomNumber.Next(0, 99)) })
             .Build();


            filmsContext.Actors.AddRange(actors);
            filmsContext.SaveChanges();
        }
    }
}
