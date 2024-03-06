using Application.CQRS.Commands.FormActions;
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

namespace GoogleFormsApiUnitTests.Controllers
{
    public class FormControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;

        private readonly Mock<IMapper> _mapperMock;

        private readonly FormController _formController;

        public FormControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _formController = new FormController(_mediatorMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateForm_WhenInvalidIdProvided_ThrowException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _formController.CreateForm(new AddFormRequest()));
        }

        [Fact]
        public async Task CreateForm_WhenCreatedSuccessfully_ReturnOkResult()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()) }));
            _formController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user
                }
            };
            var command = new Add.Command();
            _mapperMock.Setup(x => x.Map<Add.Command>(It.IsAny<AddFormRequest>())).Returns(() => command);
            _mediatorMock.Setup(x => x.Send(It.IsAny<Add.Command>(), default)).Returns(Task.CompletedTask);

            var result = await _formController.CreateForm(new AddFormRequest());

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}
