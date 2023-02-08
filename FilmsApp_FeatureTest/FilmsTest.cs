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
        public async Task Test_FIlmController_GetAllFIllms_OkResult()
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

    }
}