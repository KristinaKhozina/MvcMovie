namespace MvcMovie.Data;

public static class PaginationHelper
{
    /// <summary>
    /// A helper method that calculates the range of page numbers to display in pagination.
    /// </summary>
    /// <param name="current">The current page number.</param>
    /// <param name="total">The total number of pages.</param>
    /// <param name="range">The maximum number of pages to display.</param>
    /// <returns>A list of page numbers to display, which may contain a '-1' marker indicating skipped pages between the first/last page and the start/end of the range. </returns>
    public static List<int> GetPages(int current, int total, int range)
    {
        range = Math.Min(range, total) - 1;
        int startIndex = Math.Max(current - range / 2, 1);
        int endIndex = startIndex + range;

        if (endIndex > total)
        {
            startIndex = total - range;
            endIndex = startIndex + range;
        }

        var pages = new List<int>();

        if (startIndex > 1)
        {
            pages.Add(1);
            if (startIndex > 2) pages.Add(-1);
        }

        for (int i = startIndex; i <= endIndex; i++)
        {
            pages.Add(i);
        }

        if (endIndex < total)
        {
            if (total - endIndex > 1) pages.Add(-1);
            pages.Add(total);
        }

        return pages;
    }
}