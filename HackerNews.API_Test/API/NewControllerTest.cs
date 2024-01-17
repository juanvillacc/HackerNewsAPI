using FluentAssertions;
using HackerNews.API.Controllers;
using HackerNews.Domain.Common;
using HackerNews.Domain.Contracts;
using HackerNews.Domain.Dto;
using HackerNews.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace HackerNews.API_Test.API
{
    public class NewControllerTest
    {
        private readonly Mock<INewService> _serviceMock;
        private readonly NewController _controller;
        private IActionResult? _response;
        private const string _exceptionMessage = "my test";
        private const string _exceptionCode = "001";

        public NewControllerTest()
        {
            this._serviceMock = new Mock<INewService>();
            this._controller = new NewController(this._serviceMock.Object);
        }

        [Fact]
        public async Task GivenNewsSearch_WhenTryToGetResults_ShouldReturnNewsList()
        {
            // Arrange
            GivenNewsList();

            // Act
            this._response = await WhenGetNewsSearchResultList();

            // Assert
            ThenGetNewsListWasSuccessful();
        }


        [Fact]
        public async Task GivenNewsSearch_WhenTryToGetResults_ShouldThrowAnException()
        {
            // Arrange
            GivenNewListWithAnException();

            // Act
            async Task<IActionResult> result() => await WhenGetNewListThrowException();


            // Assert
            _ = ThenGetNewsListThrowExceptionAsync(result);
        }

        private async Task ThenGetNewsListThrowExceptionAsync(Func<Task<IActionResult>> result)
        {
            var exception = await Assert.ThrowsAsync<HackerNewsException>(result);
            _ = exception.Code.Should().Be(_exceptionCode);
            _ = exception.Message.Should().Be(_exceptionMessage);
        }

        private void ThenGetNewsListWasSuccessful()
        {
            var result = this._response as OkObjectResult;
            _ = this._response.Should().NotBeNull();
            _ = result.Should().NotBeNull();
            _ = result.Value.Should().NotBeNull();
            _ = result.Value.Should().BeOfType<PageResult<NewItemDto>>();
            _ = result.StatusCode.Should().Be((int)HttpStatusCode.OK, result.StatusCode.ToString());
        }

        private async Task<IActionResult?> WhenGetNewsSearchResultList()
        {
            var response = await _controller.Search(It.IsAny<string>(),It.IsAny<int>(), It.IsAny<int>());
            return response.Result;
        }

        private void GivenNewsList()
        {
            this._serviceMock
                .Setup(mock => mock.SearchAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((0, new List<NewItemDto>()));
        }

        private void GivenNewListWithAnException()
        {
            this._serviceMock
                .Setup(mock => mock.SearchAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new HackerNewsException(_exceptionMessage, _exceptionCode)); ;
        }

        public async Task<IActionResult?> WhenGetNewListThrowException()
        {
            var response = await _controller.Search(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>());
            return response.Result;
        }

    }
}
