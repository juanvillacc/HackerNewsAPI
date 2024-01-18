using HackerNews.Domain.Contracts;

namespace HackerNews.Service
{
    public class NewService
    {
        private readonly IStoryRepository _newRepository;
        public NewService(IStoryRepository newRepository)
        {
            _newRepository = newRepository; 
        }
    }
}