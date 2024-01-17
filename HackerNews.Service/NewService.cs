using HackerNews.Domain.Contracts;

namespace HackerNews.Service
{
    public class NewService
    {
        private readonly INewRepository _newRepository;
        public NewService(INewRepository newRepository)
        {
            _newRepository = newRepository; 
        }
    }
}