
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PSW_backend.Controllers;
using PSW_backend.Dtos;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PSW_backendTest.IntegrationTests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<PSW_backend.Startup>>
    {

        [Fact]
        public async Task Login_success()
        {
            //Arange
            var mockFactory = new Mock<IHttpClientFactory>();
            HttpClient client = MockClient(CreateLoginDto());
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            //Act
            var response = await client.PostAsync("https://localhost:44393/api/user/login", new StringContent("Ok", Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            //Assert
            responseBody.ShouldBe("{\"Username\":\"mb\",\"Password\":\"123\"}");
        }

        [Fact]
        public async Task Login_Fail()
        {
            //Arange
            var mockFactory = new Mock<IHttpClientFactory>();
            HttpClient client = MockClientNotFound();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            
            //Act
            var response = await client.PostAsync("https://localhost:44393/api/user/login", new StringContent("Bad", Encoding.UTF8, "application/json"));
            string responseBody = await response.Content.ReadAsStringAsync();

            //Assert
            responseBody.ShouldContain("Not Found");
        }

        public HttpClient MockClient(LoginDto loginDto)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(loginDto)),
                });
            return new HttpClient(mockHttpMessageHandler.Object);

        }

        public HttpClient MockClientNotFound()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Not Found"),

                });
            return new HttpClient(mockHttpMessageHandler.Object);

        }

        #region HelperFunctions
        private LoginDto CreateLoginDto()
        {
            return new LoginDto
            {
                Username = "mb",
                Password = "123",
            };
        }
        
        #endregion HelperFunctions
    }
}
