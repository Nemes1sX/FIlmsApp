using AutoMapper;
using FIlmsApp.Data;
using FIlmsApp.Infrastructure;
using FIlmsApp.Models.Entities;
using FIlmsApp.Models.FormRequest;
using FIlmsApp.Services;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;

namespace FilmsAppTest.Unit
{
    public class Tests
    {
        private DbContextOptions<FilmsContext> dbContext = new DbContextOptionsBuilder<FilmsContext>()
               .UseInMemoryDatabase(databaseName: "TestDb")
               .Options;

        private FilmService filmService;
        private IMapper _mapper;


        [SetUp]
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
            var filmsList = await filmService.GetAll();
            Assert.AreEqual(10, filmsList.Count());
        }

        [Test]
        public async Task FilmService_CreateFilm_SuccessFully()
        {
            //Act
            var filmRequest = new FilmFormRequest();
            filmRequest.Name = "name";
            filmRequest.ReleasedDate = DateTime.Now;
            filmRequest.GenreId = 1;
            filmRequest.ActorId = 1;

            //Arrange
            var film = await filmService.Create(filmRequest);
            Assert.AreEqual(filmRequest.Name, film.Name);
            Assert.AreEqual(filmRequest.ReleasedDate, film.ReleasedDate);

        }


        [Test]
        public async Task FilmService_FindFilm_Successfully()
        {
            var film = filmService.Read(1);

            Assert.IsNotNull(film);
        }

        [Test]
        public async Task FilmService_DeleteFilm_Successfully()
        {
            await filmService.Delete(1);
            var film = await filmService.Read(1);

            Assert.IsNull(film);
        }

        private void SeedFilms(FilmsContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
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