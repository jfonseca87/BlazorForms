using Blazored.Toast.Services;
using BlazorForms.Models;
using BlazorForms.Services;
using BlazorForms.Utils;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlazorTests.Services
{
    public class PersonServiceTest
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly Mock<IToastService> _toastServiceMock;
        private readonly PersonService _personService;

        public PersonServiceTest()
        {
            _toastServiceMock = new Mock<IToastService>();
            _handlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(_handlerMock.Object);
            httpClient.BaseAddress = new Uri("http://localhost:5000");
            _personService = new PersonService(httpClient, _toastServiceMock.Object);
        }

        [Fact]
        public async Task GetPeopleSuccessFull()
        {
            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(new List<Person> { new Person(), new Person() }))
               });

            var result = await _personService.GetPeople();

            Assert.NotNull(result);
            Assert.Equal(ResponseStatus.Success, result.Status);
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public async Task GetPeopleFailReturningInternalServerError ()
        {
            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.InternalServerError,
                   Content = new StringContent("An error ocurred getting people")
               });

            var result = await _personService.GetPeople();

            Assert.NotNull(result);
            Assert.Equal(ResponseStatus.Error, result.Status);
            Assert.Equal("An error ocurred getting people", result.Errors[0]);
            Assert.Empty(result.Data);
        }
    }
}
