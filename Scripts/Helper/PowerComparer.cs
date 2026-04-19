using System.Collections.Generic;

public sealed class PowerComparer<T> : IComparer<T> where T : IPowerSortable
{
    public static readonly PowerComparer<T> Desc = new(true);
    public static readonly PowerComparer<T> Asc  = new(false);

    private readonly bool _descending;

    private PowerComparer(bool descending)
    {
        _descending = descending;
    }

    public int Compare(T a, T b)
    {
        // Desc: b - a | Asc: a - b
        return _descending
            ? b.Power.CompareTo(a.Power)
            : a.Power.CompareTo(b.Power);
    }
}
