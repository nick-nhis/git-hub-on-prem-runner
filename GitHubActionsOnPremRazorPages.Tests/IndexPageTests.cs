using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;

namespace GitHubActionsOnPremRazorPages.Tests
{
    public class IndexPageTests : IClassFixture<WebApplicationFactory<Program>>
    {

        // to set the test.runsettings file via command line
        // could be useful if need to include in CI/CD pipeline
        //  dotnet test --settings test.runsettings

        private readonly WebApplicationFactory<Program> _factory;

        public IndexPageTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PageLoadsSuccessfully()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Content.Headers.ContentType?.MediaType == "text/html");
        }

        [Fact]
        public async Task PageContainsWelcomeHeading()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/");
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("<h1>Welcome</h1>", content);
        }
    }
}