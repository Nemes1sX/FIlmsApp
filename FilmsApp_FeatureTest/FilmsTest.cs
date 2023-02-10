using FIlmsApp.Controllers;
using FIlmsApp.Models.Dtos;
using FIlmsApp.Models.FormRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace FilmsApp_FeatureTest
{
    public class FilmsTest : BaseControllerTest
    {

        public FilmsTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }


        [Fact]
        public async Task FIlmController_GetAllFIlms_OkResult()
        {
            var client = this.GetNewClient();

            var response = await client.GetAsync("api/films/index");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<FilmDto>>(stringResponse).ToList();
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("OK", statusCode);
            Assert.True(result.Count == 10);
        }

        [Fact]
        public async Task FIlmController_GetFilm_OkResult()
        {
            var client = this.GetNewClient();
            var id = 1;

            var response = await client.GetAsync($"api/films/get?id={id}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilmDto>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("OK", statusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FIlmController_GetFilm_NotFound()
        {
            var client = this.GetNewClient();
            var id = 101;

            var response = await client.GetAsync($"api/films/get?id={id}");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilmDto>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("NotFound", statusCode);
            Assert.Equal(0, result.Id);

        }

        [Fact]
        public async Task FIlmController_CreateFilm_BadRequest()
        {
            var filmRequest = new StoreFilmFormRequest();
            filmRequest.Name = string.Empty;
            filmRequest.ActorId = 0;
            filmRequest.GenreId = 0;
            filmRequest.ReleasedDate = DateTime.Today;
            var client = this.GetNewClient();

            var response = await client.PostAsJsonAsync("api/films/create", filmRequest);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilmDto>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("BadRequest", statusCode);
        }

        [Fact]
        public async Task FIlmController_CreateFilm_OkResult()
        {
            var filmRequest = new StoreFilmFormRequest();
            filmRequest.Name =  "Test";
            filmRequest.ActorId = 1;
            filmRequest.GenreId = 1;
            filmRequest.ReleasedDate = DateTime.Today;
            var client = this.GetNewClient();

            var response = await client.PostAsJsonAsync("api/films/create", filmRequest);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilmDto>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal(filmRequest.Name, result.Name);
            Assert.Equal("OK", statusCode);
        }

        [Fact]
        public async Task FIlmController_UpdateFilm_BadRequest()
        {
            var id = 2;
            var filmRequest = new StoreFilmFormRequest();
            filmRequest.Name = string.Empty;
            filmRequest.ActorId = 0;
            filmRequest.GenreId = 0;
            filmRequest.ReleasedDate = DateTime.Today;
            var client = this.GetNewClient();

            var response = await client.PutAsJsonAsync($"api/films/update?id={id}", filmRequest);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilmDto>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("BadRequest", statusCode);
        }

        [Fact]
        public async Task FIlmController_UpdateFilm_NotFound()
        {
            var id = 501;
            var filmRequest = new StoreFilmFormRequest();
            filmRequest.Name = "Test1";
            filmRequest.ActorId = 1;
            filmRequest.GenreId = 1;
            filmRequest.ReleasedDate = DateTime.Today;
            var client = this.GetNewClient();

            var response = await client.PutAsJsonAsync($"api/films/update?id={id}", filmRequest);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilmDto>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("NotFound", statusCode);
        }

        [Fact]
        public async Task FIlmController_UpdateFilm_OkResult()
        {
            var id = 2;
            var filmRequest = new StoreFilmFormRequest();
            filmRequest.Name = "Test1";
            filmRequest.ActorId = 1;
            filmRequest.GenreId = 1;
            filmRequest.ReleasedDate = DateTime.Today;
            var client = this.GetNewClient();

            var response = await client.PutAsJsonAsync($"api/films/update?id={id}", filmRequest);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilmDto>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal(filmRequest.Name, result.Name);
            Assert.Equal("OK", statusCode);
        }

        [Fact]
        public async Task FIlmController_DeleteFilm_OKResult()
        {
            var id = 5;
            var client = this.GetNewClient();

            var response = await client.DeleteAsync($"api/films/delete?id={id}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<object>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("OK", statusCode);
        }

        [Fact]
        public async Task FIlmController_DeleteFilm_NotFound()
        {
            var id = 501;
            var client = this.GetNewClient();

            var response = await client.DeleteAsync($"api/films/delete?id={id}");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<object>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("NotFound", statusCode);
        }
    }
}