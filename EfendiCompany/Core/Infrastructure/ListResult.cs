namespace Core.Infrastructure;

public class ListResult<T> where T : class
{
    public IEnumerable<T> Data { get; set; }
    public int TotalCount { get; set; }
}

