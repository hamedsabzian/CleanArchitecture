using Microsoft.EntityFrameworkCore;
using Todo.Application.Shared.Dtos;

namespace Todo.Infrastructure.Data.Extensions;

internal static class PaginationExtensions
{
    public static async Task<PaginatedList<T>> PaginateAsync<T>(
        this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
