namespace HackerNews.Domain.Common
{
    public static class ApiLiterals
    {
        public const string SearchSummary = "News list based on the parameters has been successfully returned";
        public const string Description401Unauthorized = "Requested resource requires authentication";
        public const string Description403Forbidden = "Authentication successful, but access is denied due to insufficient privileges";
        public const string Description404NotFound = "Requested resource was not found";
        public const string Description500InternalServerError = "UnexpectedError";
    }
}
