using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace NetCoreIntro.Tests
{
    public class EndToEndTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly WebApplicationFactoryClientOptions _options;

        public EndToEndTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _options = new WebApplicationFactoryClientOptions {BaseAddress = new Uri("http://localhost:5000")};
        }

        [Fact]
        public async Task ShouldAddAndGetData()
        {
            var client = _factory.CreateClient(_options);
            client.DefaultRequestHeaders.Add("ClientId", "Microsoft");

            var postContent = new StringContent(
                @"{'Country':'Kenya', 'Varietal':'SL28', 'Humidity':0.1}",
                Encoding.UTF8,
                "application/json");

            await client.PostAsync("/api/coffee", postContent);
            var response = await client.GetAsync("/api/coffee");
            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            content.Should().Contain("Kenya");
        }
    }
}