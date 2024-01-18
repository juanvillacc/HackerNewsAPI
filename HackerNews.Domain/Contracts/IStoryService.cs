using HackerNews.Domain.Dto;

namespace HackerNews.Domain.Contracts
{
    public interface IStoryService
    {
        Task<(int count, List<StoryResponseDto> data)> SearchAsync(string title, int pageSize, int pageIndex);
    }
}
