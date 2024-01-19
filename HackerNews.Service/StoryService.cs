using HackerNews.Domain.Contracts;
using HackerNews.Domain.Dto;
using HackerNews.Domain.Models;

namespace HackerNews.Service
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _storyRepository;
        public StoryService(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<(int count, List<StoryResponseDto> data)> SearchAsync(string title, int pageSize, int pageIndex)
        {
            var (count, data) = await _storyRepository.SearchAsync(title, pageSize, pageIndex);

            return (count, data.Select(x => new StoryResponseDto()
            {
                Title = x.Title,
                Id = x.Id,
                Link = x.Link
            }).ToList());
        }

        public async Task SynchronizeAsync()
        {
            var newStories = await _storyRepository.GetNewStories();
            var nonExistingIds = await _storyRepository.GetNonExistingStories(newStories);
            var newStoriesList = new List<Story>();
            foreach (var id in nonExistingIds)
            {
                var storyDetail = await _storyRepository.GetStoryDetail(id);
                if (storyDetail.Type.Equals("story"))
                {
                    newStoriesList.Add(new Story()
                    {
                        Id = storyDetail.Id,
                        Title = storyDetail.Title,
                        Link = storyDetail.Url,
                    });
                }
            }

            await _storyRepository.BulkInsert(newStoriesList);
        }
    }
}