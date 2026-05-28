using System.Collections.Generic;

public static class ListSortHelper
{
    public static void SortByPower<T>(List<T> list,bool descending = true) where T : IPowerSortable
    {
        list.Sort(
            descending
                ? PowerComparer<T>.Desc
                : PowerComparer<T>.Asc
        );
    }
}
