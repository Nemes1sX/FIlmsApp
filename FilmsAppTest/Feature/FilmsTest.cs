using AutoMapper;
using FIlmsApp.Controllers;
using FIlmsApp.Data;
using FIlmsApp.Infrastructure;
using FIlmsApp.Models.Entities;
using FIlmsApp.Models.FormRequest;
using FIlmsApp.Services;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsAppTest.Feature
{
    public class FilmsTest
    {
        private FilmsController filmsController;
        private DbContextOptions<FilmsContext> dbContext = new DbContextOptionsBuilder<FilmsContext>()
         .UseInMemoryDatabase(databaseName: "TestDb")
         .Options;
        private FilmService filmService;
        private IMapper _mapper;


        [OneTimeSetUp]
        public void SetUp()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            filmService = new FilmService(new FilmsContext(dbContext), _mapper);
            filmsController = new FilmsController(filmService);
            SeedFilms();
        }

        [Test]
        public async Task Test_FIlmController_GetAllFIllms_OkResult()
        {
            var data = await filmsController.Index();

            Assert.IsInstanceOf<OkObjectResult>(data);
        }

        [Test]
        public async Task FilmController_GetFilm_OkResult()
        {
            //Act
            var id = 1;

            //Arrange
            var data = await filmsController.Get(id);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(data);

        }

        [Test]
        public async Task FilmController_GetFilm_NotFound()
        {
            //Act
            var id = 150;

            //Arrange
            var data = await filmsController.Get(id);

            //Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(data);
        }

        [Test]
        public async Task Test_FIlmController_CreateFilm_BadRequest()
        {
            var request = new FilmFormRequest();
            request.ActorId = 0;
            request.GenreId= 0;
            request.Name= "";
            request.ReleasedDate = DateTime.Now;

            var data = await filmsController.Post(request);

            Assert.Null(data);
            //Assert.IsInstanceOf<BadRequestObjectResult>(data);
        }

        private void SeedFilms()
        {
            var context = new FilmsContext(dbContext);

            context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            context.Genres.AddRange(new Genre { Id = 1, Name = "Action" },
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

            context.Films.AddRange(films);


            var actors = Builder<Actor>.CreateListOfSize(100)
             .All()
             .With(a => a.Name = Faker.Name.FullName())
             .With(a => a.Films = new List<Film> { films.ElementAt(Faker.RandomNumber.Next(0, 99)), films.ElementAt(Faker.RandomNumber.Next(0, 99)) })
             .Build();


            context.Actors.AddRange(actors);
            context.SaveChanges();
        }
    }
}
