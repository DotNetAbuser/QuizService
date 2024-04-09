namespace Shared.Responses;

public class PaginatedDataResponse<T>(IEnumerable<T> list, int totalCount)
{
    public IEnumerable<T> List { get; set; } = list;
    public int TotalCount { get; set; } = totalCount;
}