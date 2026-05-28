using Constants;

namespace TimeTracker.API.Extensions;

public static class OffsetPaginationExtensions
{
    public static OffsetPagination ToOffsetPagination(int limit)
    {
        return new OffsetPagination(0, limit);
    }
}