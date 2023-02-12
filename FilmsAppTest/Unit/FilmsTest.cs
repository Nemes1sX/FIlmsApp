using AutoMapper;
using FIlmsApp.Data;
using FIlmsApp.Infrastructure;
using FIlmsApp.Models.Entities;
using FIlmsApp.Models.FormRequest;
using FIlmsApp.Services;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FilmsAppTest.Unit
{
    public class FilmsTest
    {
        private DbContextOptions<FilmsContext> dbContext = new DbContextOptionsBuilder<FilmsContext>()
               .UseInMemoryDatabase(databaseName: "TestDb")
               .Options;

        private FilmService filmService;
        private IMapper _mapper;


        [OneTimeSetUp]
        public void Setup()
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
            SeedFilms();
        }

        [Test]
        public async Task FilmsService_GetAllFilms_Succesfully()
        {
            //Arrange
            var filmsList = await filmService.GetAll();

            //Assert
            Assert.AreEqual(10, filmsList.Count());
        }

        [Test]
        public async Task FilmService_CreateFilm_SuccessFully()
        {
            //Act
            var filmRequest = new StoreFilmFormRequest();
            filmRequest.Name = "name";
            filmRequest.ReleasedDate = DateTime.Now;
            filmRequest.GenreId = 1;
            filmRequest.ActorId = 1;

            //Arrange
            var film = await filmService.Create(filmRequest);

            //Assert
            Assert.AreEqual(filmRequest.Name, film.Name);
            Assert.AreEqual(filmRequest.ReleasedDate, film.ReleasedDate);

        }


        [Test]
        public async Task FilmService_FindFilm_Successfully()
        {
            //Act
            var id = 12;

            //Arrange
            var film = await filmService.Read(id);

            //Assert
            Assert.IsNotNull(film);
        }

        [Test]
        public async Task FIlmService_UpdateFilm_Successfully()
        {
            //Act 
            var id = 101;
            var filmFormRequest = new StoreFilmFormRequest();
            filmFormRequest.Name = "name1";
            

            //Arrange
            var film = await filmService.Update(id, filmFormRequest);

            //Assert
            Assert.AreEqual(filmFormRequest.Name, film.Name);
        }

        [Test]
        public async Task FIlmService_UpdateFilm_Failed()
        {
            //Act 
            var id = 501;
            var filmFormRequest = new StoreFilmFormRequest();
            filmFormRequest.Name = "name1";


            //Arrange
            var film = await filmService.Update(id, filmFormRequest);

            //Assert
            Assert.IsNull(film);
        }

        [Test]
        public async Task FilmService_DeleteFilm_Successfully()
        {
            //Act
            var id = 1;

            //Arrange 
            await filmService.Delete(id);
            var film = await filmService.Read(id);

            //Assert
            Assert.IsNull(film);
        }

        [Test]
        public async Task FilmService_DeleteFilm_Failed()
        {
            //Act
            var id = 501;

            //Arrange
            var deletedId = await filmService.Delete(id);

            //Assert
            Assert.Zero(deletedId);
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