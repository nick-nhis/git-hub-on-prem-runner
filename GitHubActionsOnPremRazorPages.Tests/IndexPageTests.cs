using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace GitHubActionsOnPremRazorPages.Tests
{
    // Integration tests for the Razor Pages app.
    // Uses WebApplicationFactory<Program> to host the application in-memory
    // and HttpClient to exercise endpoints/pages.
    public class IndexPageTests : IClassFixture<WebApplicationFactory<Program>>
    {

        // Note: you can pass a runsettings file to dotnet test to disable
        // shadow-copying / appdomain if required:
        //   dotnet test --settings Test.runsettings

        // WebApplicationFactory is provided by xUnit via IClassFixture<T>.
        // It creates and manages the test server lifetime for the class.
        private readonly WebApplicationFactory<Program> _factory;

        // Constructor receives the shared WebApplicationFactory instance.
        // Keep constructor lightweight — the factory is expensive to create,
        // so xUnit will reuse the same instance for the test class.
        public IndexPageTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        // Verifies the root Index page returns 200 OK and HTML content.
        // Arrange: create HttpClient from the factory.
        // Act: send GET request to "/".
        // Assert: status is OK and content type is HTML.
        [Fact]
        public async Task PageLoadsSuccessfully()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Content.Headers.ContentType?.MediaType == "text/html");
        }

        // Verifies the Index page contains a known heading.
        // This helps ensure the Razor page rendered correctly and expected content is present.
        [Fact]
        public async Task PageContainsWelcomeHeading()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/");
            var content = await response.Content.ReadAsStringAsync();

            // Replace the expected string with the actual text from your Index.cshtml if different.
            Assert.Contains("<h1>Welcome</h1>", content);
        }

        // Parameterized test to ensure other important pages also load successfully.
        // Makes it easy to add more routes for quick smoke checks.
        [Theory]
        [InlineData("/Privacy")]
        [InlineData("/Error")]
        public async Task OtherPagesLoadSuccessfully(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}