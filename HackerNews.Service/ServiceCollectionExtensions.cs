using HackerNews.Domain.Contracts;
using HackerNews.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStoryService, StoryService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStoryRepository, StoryRepository>();
        }
    }
}
