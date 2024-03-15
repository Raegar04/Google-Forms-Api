using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleFormsApi.IntegrationTesting.ControllersTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public Mock<IMediator> MediatorMock { get; set; }

        public Mock<IMapper> MapperMock { get; set; }

        public CustomWebApplicationFactory()
        {
            MediatorMock = new Mock<IMediator>();
            MapperMock = new Mock<IMapper>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
            //Overriding the DI mechanism in Asp.Net core
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

                services.AddSingleton(MediatorMock.Object);
                services.AddSingleton(MapperMock.Object);

            });
        }
    }
}
