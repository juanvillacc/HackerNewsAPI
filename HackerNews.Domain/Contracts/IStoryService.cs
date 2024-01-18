using HackerNews.Domain.Dto;
using HackerNews.Domain.Models;

namespace HackerNews.Domain.Contracts
{
    public interface IStoryService
    {
        Task<(int count, List<StoryResponseDto> data)> SearchAsync(string title, int pageSize, int pageIndex);
        Task SynchronizeAsync();
       
    }
}
