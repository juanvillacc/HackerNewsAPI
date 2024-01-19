using HackerNews.Domain.Common;
using HackerNews.Domain.Contracts;
using HackerNews.Domain.Dto;
using HackerNews.Domain.Models;
using HackerNews.Helpers;
using HackerNews.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HackerNews.Repository
{
    public class StoryRepository : IStoryRepository
    {

        protected readonly ApplicationDbContext _context;

        public StoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<(int count, List<Story> data)> SearchAsync(string title, int pageSize, int pageIndex)
        {
            var total = _context.Story.Where(x => x.Title.Contains(title) || title.Equals("")).Count();
            var results = await _context.Story.Where(x => x.Title.Contains(title) || title.Equals("")).Skip(pageIndex).Take(pageSize).ToListAsync();

            return (total, results);
        }

        public async Task<List<int>> GetNonExistingStories(List<int> ids)
        {
            var existingIds = await _context.Story.Where(x => ids.Contains(x.Id)).Select(y => y.Id).ToListAsync();
            return ids.FindAll(x => !existingIds.Contains(x)).ToList();
        }

        public async Task<List<int>> GetNewStories()
        {
            var list = await HttpClientWrapper<List<Int32>>.Get(IntegrationLiterals.TopStoriesUrl);
            return list;
        }

        public async Task<GetItemDetailDto> GetStoryDetail(int Id)
        {
            
            var detail = await HttpClientWrapper<GetItemDetailDto>.Get($"https://hacker-news.firebaseio.com/v0/item/{Id}.json?print=pretty");
            return detail;
        }

        public async Task BulkInsert(List<Story> newStoriesList)
        {
            await _context.Story.AddRangeAsync(newStoriesList);
            await _context.SaveChangesAsync();
        }
    }
}