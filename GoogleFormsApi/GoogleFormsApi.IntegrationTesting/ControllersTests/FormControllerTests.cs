using AutoMapper;
using GoogleFormsApi.Controllers;
using GoogleFormsApi.Requests.Form;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GoogleFormsApi.IntegrationTesting.ControllersTests
{
    public class FormControllerTests
        : IDisposable // Need to dispose CustomWebApplicationFactory and HttpClient
    {
        private readonly CustomWebApplicationFactory _factory;

        private readonly HttpClient _httpClient;

        public FormControllerTests()
        {
            _factory = new CustomWebApplicationFactory();
            _httpClient = _factory.CreateClient();
        }

        //[Fact]
        //public async Task GetFormById_WhenGotSuccessfully_ShouldReturnOkObjectResultWithReceivedData()
        //{
        //    var testId = Guid.NewGuid();
        //    var response = await _httpClient.GetAsync("api/Form/"+ testId);

        //    Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        //}

        public void Dispose()
        {
            _httpClient.Dispose();
            _factory.Dispose();
        }
    }
}
