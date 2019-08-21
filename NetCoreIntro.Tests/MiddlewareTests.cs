using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace NetCoreIntro.Tests
{
    public class MiddlewareTests : IClassFixture<WebApplicationFactory<Startup>>
    {
    }
}