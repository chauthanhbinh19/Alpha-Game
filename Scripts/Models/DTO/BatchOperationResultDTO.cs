using System.Collections.Generic;

public class BatchOperationResultDTO<T>
{
    public List<T> InsertedItems { get; set; } = new List<T>();
    public List<T> UpdatedItems { get; set; } = new List<T>();

    public bool HasChanges => InsertedItems.Count > 0 || UpdatedItems.Count > 0;
}