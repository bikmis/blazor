﻿using Bunit;
using Intel.EmployeeManagement.BlazorClient.Tests.Services;
using Intel.EmployeeManagement.RazorClassLibrary.Pages.TestExamplePage;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Photo_Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests
{
    //https://docs.microsoft.com/en-us/aspnet/core/blazor/test?view=aspnetcore-5.0

    public class TestExampleTests : TestContext
    {
        [Fact]
        public void NumberOfPhotosIs3()
        {
            var configurationService = new ServiceDescriptor(typeof(IConfiguration), new MockConfigurationService());
            var httpClientService = new ServiceDescriptor(typeof(HttpClient), new HttpClient());
            var photoService = new ServiceDescriptor(typeof(IPhotoService), new MockPhotoService());
            Services.Add(configurationService);
            Services.Add(httpClientService);
            Services.Add(photoService);

            var cut = RenderComponent<TestExample1>();
            cut.Find("#getPhotos_1").Click();
            var text = cut.Find("#countOfPhoto").TextContent;
            text.MarkupMatches("Count of photos: 3");
        }

        [Fact]
        public void NumberOfPhotosIs5000()
        {
            var configurationService = new ServiceDescriptor(typeof(IConfiguration), new MockConfigurationService());
            var httpClientService = new ServiceDescriptor(typeof(HttpClient), new HttpClient());
            var photoService = new ServiceDescriptor(typeof(IPhotoService), new MockPhotoService());
            Services.Add(configurationService);
            Services.Add(httpClientService);
            Services.Add(photoService);

            var cut = RenderComponent<TestExample1>();
            cut.Find("#getPhotos_2").Click();
            Thread.Sleep(5000);
            var text = cut.Find("#countOfPhoto").TextContent;
            text.MarkupMatches("Count of photos: 5000");
        }
    }
}
