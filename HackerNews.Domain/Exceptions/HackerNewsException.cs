namespace HackerNews.Domain.Exceptions
{
    public class HackerNewsException : Exception
    {
        public string Code { get; set; }

        public HackerNewsException(string message, string code) : base(message)
        {
            Code = code;
        }
    }
}
