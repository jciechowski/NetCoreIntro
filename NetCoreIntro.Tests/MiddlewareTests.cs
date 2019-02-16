using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace NetCoreIntro.Tests
{
    public class MiddlewareTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public MiddlewareTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _options = new WebApplicationFactoryClientOptions {BaseAddress = new Uri("http://localhost:5000")};
        }

        private readonly WebApplicationFactory<Startup> _factory;
        private readonly WebApplicationFactoryClientOptions _options;

        [Fact]
        public async Task ShouldCallPingWithoutClientId()
        {
            var client = _factory.CreateClient(_options);

            var response = await client.GetAsync("/ping");
            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            content.Should().Contain("Service is working");
        }

        [Fact]
        public async Task ShouldNotReturnDataForWrongClientId()
        {
            var client = _factory.CreateClient(_options);

            var response = await client.GetAsync("/api/coffee");
            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            content.Should().Contain("Client not allowed");
        }

        [Fact]
        public async Task ShouldReturnCorrectDataForCorrectClientId()
        {
            var client = _factory.CreateClient(_options);
            client.DefaultRequestHeaders.Add("ClientId", "PGS");

            var response = await client.GetAsync("/api/coffee");
            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            content.Should().Be("[]");
        }
    }
}