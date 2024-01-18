using HackerNews.Domain.Dto;
using HackerNews.Domain.Models;

namespace HackerNews.Domain.Contracts
{
    public interface IStoryRepository
    {
        Task<(int count, List<Story> data)> SearchAsync(string title, int pageSize, int pageIndex);
        Task<List<int>> GetNonExistingStories(List<int> ids);
        Task<List<int>> GetNewStories();
        Task<GetItemDetailDto> GetStoryDetail(int id);
        Task BulkInsert(List<Story> newStoriesList);
    }
}
