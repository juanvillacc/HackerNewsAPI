using HackerNews.Domain.Dto;

namespace HackerNews.Domain.Contracts
{
    public interface INewService
    {
        Task<(int count, List<NewItemDto> data)> SearchAsync(string title, int pageSize, int pageIndex);
    }
}
