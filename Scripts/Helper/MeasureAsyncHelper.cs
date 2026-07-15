using System.Diagnostics;
using System.Threading.Tasks;

public static class MeasureAsyncHelper
{
    // public static async Task<T> MeasureAsyncHelper<T>(string name, Task<T> task)
    // {
    //     var sw = Stopwatch.StartNew();

    //     try
    //     {
    //         var result = await task;
    //         Debug.Log($"{name}: {sw.ElapsedMilliseconds} ms");
    //         return result;
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"{name}: ERROR - {ex.Message}");
    //         throw;
    //     }
    // }
}