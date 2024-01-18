using FluentAssertions;
using HackerNews.Domain.Contracts;
using HackerNews.Domain.Dto;
using HackerNews.Domain.Models;
using HackerNews.Service;
using Moq;

namespace HackerNews.API_Test.Service
{
    public class StoryServiceTests
    {
        private readonly Mock<IStoryRepository> _mockStoryRepository = new();

        [Fact]
        public async Task GivenNewSearch_WhenTryToGetResults_ShouldReturnCorrectData()
        {
            // Arrange
            
            GivenListResult();

            var storyService = new StoryService(_mockStoryRepository.Object);

            // Act
            var result = await WhenGetTheResults(storyService);

            // Assert
            ThenGetResultListWasSuccessful(result);
        }

        private static void ThenGetResultListWasSuccessful((int count, List<StoryResponseDto> data) result)
        {
            result.count.Should().Be(5);
            result.data.Count.Should().Be(5);
          
            // Verify that the transformation from Story to StoryResponseDto is correct.
            
            result.data[0].Title.Should().Be("Title1");
            result.data[0].Id.Should().Be(1);
            result.data[0].Link.Should().Be("Link1");
        }

        private static async Task<(int count, List<StoryResponseDto> data)> WhenGetTheResults(StoryService storyService)
        {
            var result = await storyService.SearchAsync("test", 10, 1);
            return result;
        }


        private void GivenListResult()
        {
            _mockStoryRepository
                .Setup(repo => repo.SearchAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((5, new List<Story>
                {
                    new Story { Title = "Title1", Id = 1, Link = "Link1" },
                    new Story { Title = "Title2", Id = 2, Link = "Link2" },
                    new Story { Title = "Title3", Id = 1, Link = "Link3" },
                    new Story { Title = "Title4", Id = 2, Link = "Link4" },
                    new Story { Title = "Title5", Id = 2, Link = "Link5" },

                }));
        }

        [Fact]
        public async Task GivenSynchronizationRequest_SynchronizationProcessShoulWorksWell()
        {
            // Arrange
            GivenAllListToProcess();

            var storyService = new StoryService(_mockStoryRepository.Object);

            // Act
            await WhenTryToSynchronize(storyService);

            // Assert
            TheSynchronizationWasSuccessful();
        }

        private void TheSynchronizationWasSuccessful()
        {
            _mockStoryRepository.Verify(repo => repo.BulkInsert(It.IsAny<List<Story>>()), Times.Once);
        }

        private static async Task WhenTryToSynchronize(StoryService storyService)
        {
            await storyService.SynchronizeAsync();
        }

        private void GivenAllListToProcess()
        {
            _mockStoryRepository
                .Setup(repo => repo.GetNewStories())
                .ReturnsAsync(new List<int> { 1, 2, 3 });

            _mockStoryRepository
                .Setup(repo => repo.GetNonExistingStories(It.IsAny<List<int>>()))
                .ReturnsAsync(new List<int> { 2, 3 }); // Assuming 2 and 3 are non-existing

            _mockStoryRepository
                .Setup(repo => repo.GetStoryDetail(It.IsAny<int>()))
                .ReturnsAsync(new GetItemDetailDto
                {
                    Id = 1,
                    Title = "Title1",
                    Url = "Link1"
                });
        }
    }
}
