using FIlmsApp.Controllers;
using FIlmsApp.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;

namespace FilmsApp_FeatureTest
{
    public class FilmsTest : BaseControllerTest
    {

        public FilmsTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }


        [Fact]
        public async Task Test_FIlmController_GetAllFIlms_OkResult()
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
        public async Task Test_FIlmController_GetFilm_OkResult()
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
        public async Task Test_FIlmController_GetFilm_NotFound()
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
    }
}